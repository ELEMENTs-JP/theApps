using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class GeminiService : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    private const string ModelId = "gemini-3.5-flash-lite";
    private const string BaseUrl = $"https://generativelanguage.googleapis.com/v1/models/{ModelId}:generateContent";

    public GeminiService(HttpClient httpClient, string key)
    {
        _httpClient = httpClient;
        _apiKey = key;
    }

    public async Task<string> GetResponseAsync(List<ChatMessage> history)
    {
        var requestBody = new
        {
            contents = history.Select(m => new
            {
                role = m.Role.ToLower() == "user" ? "user" : "model",
                parts = new[] { new { text = m.Text } }
            }).ToArray()
        };

        try
        {
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BaseUrl}?key={_apiKey}", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"API Fehler ({response.StatusCode}): {responseBody}";
            }

            var result = JsonSerializer.Deserialize<GeminiResponse>(responseBody);
            return result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text
                   ?? "Fehler: Antwort-Struktur ungültig.";
        }
        catch (Exception ex)
        {
            return $"Verbindungsfehler: {ex.Message}";
        }
    }

    public async Task<List<Section>> GetStructuredResponseAsync(List<ChatMessage> history)
    {
        string url = $"{BaseUrl}?key={_apiKey}";
        string systemPrompt = "ANWEISUNG: Analysiere die Anfrage und strukturiere das Ergebnis in logische Abschnitte.\n\n";

        var contents = new List<object>();

        for (int i = 0; i < history.Count; i++)
        {
            string text = history[i].Text;
            if (i == 0)
                text = systemPrompt + text;

            contents.Add(new
            {
                role = history[i].Role.ToLower() == "user" ? "user" : "model",
                parts = new[] { new { text = text } }
            });
        }

        var requestBody = new
        {
            contents = contents.ToArray(),
            generationConfig = new
            {
                responseMimeType = "application/json",
                responseSchema = new
                {
                    type = "ARRAY",
                    items = new
                    {
                        type = "OBJECT",
                        properties = new
                        {
                            title = new { type = "STRING" },
                            content = new { type = "STRING" }
                        },
                        required = new[] { "title", "content" }
                    }
                }
            }
        };

        int maxRetries = 3;
        int delayMs = 1000;

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                var jsonRequest = JsonSerializer.Serialize(requestBody);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, httpContent);
                var responseBody = await response.Content.ReadAsStringAsync();

                if ((int)response.StatusCode == 429 && attempt < maxRetries)
                {
                    await Task.Delay(delayMs);
                    delayMs *= 2;
                    continue;
                }

                if (!response.IsSuccessStatusCode)
                {
                    switch ((int)response.StatusCode)
                    {
                        case 429:
                        return new List<Section> {
                                new Section { Title = "API-Limit", Content = "Kontingent erschöpft (429). Bitte warte eine Minute oder prüfe dein Tageslimit." }
                            };
                        case 401:
                        case 403:
                        return new List<Section> {
                                new Section { Title = "Authentifizierung", Content = "API-Key ungültig oder keine Berechtigung (401/403)." }
                            };
                        default:
                        return new List<Section> {
                                new Section { Title = $"API Fehler {(int)response.StatusCode}", Content = responseBody }
                            };
                    }
                }

                var result = JsonSerializer.Deserialize<GeminiResponse>(responseBody);
                var rawJsonText = result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

                if (string.IsNullOrEmpty(rawJsonText))
                {
                    return new List<Section> { new Section { Title = "Hinweis", Content = "Keine Daten empfangen." } };
                }

                var sections = JsonSerializer.Deserialize<List<Section>>(rawJsonText);
                return sections ?? new List<Section>();
            }
            catch (Exception ex)
            {
                if (attempt == maxRetries)
                {
                    return new List<Section> {
                        new Section { Title = "Verarbeitungsfehler", Content = ex.Message }
                    };
                }
                await Task.Delay(delayMs);
            }
        }

        return new List<Section> { new Section { Title = "Timeout", Content = "Maximale Anzahl an Versuchen überschritten." } };
    }

    public async Task<byte[]> GenerateImageAsync(string prompt)
    {
        const string imageModelId = "gemini-nano-banana-2.1";
        string url = $"https://generativelanguage.googleapis.com/v1/models/{imageModelId}:generateContent?key={_apiKey}";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[] { new { text = prompt } }
                }
            },
            generationConfig = new
            {
                responseModalities = new[] { "IMAGE" },
                imageConfig = new
                {
                    aspectRatio = "16:9",
                    imageSize = "2K"
                }
            }
        };

        try
        {
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API Fehler ({response.StatusCode}): {responseBody}");
            }

            var result = JsonSerializer.Deserialize<GeminiResponse>(responseBody);
            var inlineData = result?.Candidates?.FirstOrDefault()?.Content?.Parts?
                .FirstOrDefault(p => p.InlineData != null)?.InlineData;

            if (inlineData != null && !string.IsNullOrEmpty(inlineData.Data))
            {
                return Convert.FromBase64String(inlineData.Data);
            }

            throw new Exception("Keine Bilddaten in der Antwort enthalten.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Bildgenerierung fehlgeschlagen: {ex.Message}");
        }
    }

    public async Task<byte[]> GenerateTextToSpeechAsync(string textToSpeak)
    {
        const string ttsModelId = "gemini-3.8-flash-tts";
        string url = $"https://generativelanguage.googleapis.com/v1/models/{ttsModelId}:generateContent?key={_apiKey}";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    role = "user",
                    parts = new[] { new { text = textToSpeak } }
                }
            },
            generationConfig = new
            {
                responseModalities = new[] { "AUDIO" }
            }
        };

        try
        {
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API Fehler ({response.StatusCode}): {responseBody}");
            }

            var result = JsonSerializer.Deserialize<GeminiResponse>(responseBody);
            var inlineData = result?.Candidates?.FirstOrDefault()?.Content?.Parts?
                .FirstOrDefault(p => p.InlineData != null)?.InlineData;

            if (inlineData != null && !string.IsNullOrEmpty(inlineData.Data))
            {
                return Convert.FromBase64String(inlineData.Data);
            }

            throw new Exception("Keine Audio-Daten in der Antwort enthalten.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Sprachgenerierung fehlgeschlagen: {ex.Message}");
        }
    }

    public class GeminiResponse
    {
        [JsonPropertyName("candidates")] public List<Candidate>? Candidates { get; set; }
    }
    public class Candidate
    {
        [JsonPropertyName("content")] public Content? Content { get; set; }
    }
    public class Content
    {
        [JsonPropertyName("parts")] public List<Part>? Parts { get; set; }
    }
    public class Part
    {
        [JsonPropertyName("text")] public string? Text { get; set; }
        [JsonPropertyName("inlineData")] public InlineData? InlineData { get; set; }
    }
    public class InlineData
    {
        [JsonPropertyName("mimeType")] public string? MimeType { get; set; }
        [JsonPropertyName("data")] public string? Data { get; set; }
    }

    public void Dispose()
    {
        try
        {
        }
        catch (Exception ex)
        {
        }
    }
}

public class ChatMessage
{
    public string Role { get; set; } = "";
    public string Text { get; set; } = "";
}

public class Section
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("content")]
    public string Content { get; set; } = "";
}