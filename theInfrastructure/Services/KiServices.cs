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

    // Korrektur: /v1/ wurde in der URL ergänzt
    //private const string ModelId = "gemini-2.5-flash";
    //private const string BaseUrl = $"https://generativelanguage.googleapis.com/v1/models/{ModelId}:generateContent";

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
        // Korrektur: Verwendet nun konsistent die korrigierte BaseUrl inklusive /v1/
        string url = $"{BaseUrl}?key={_apiKey}";

        var parts = new List<object>();
        string systemPrompt = "ANWEISUNG: Antworte NUR im JSON-Format als Liste von Objekten mit den Feldern 'title' und 'content'.\n\n";

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

        var requestBody = new { contents = contents.ToArray() };

        try
        {
            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, httpContent);
            var responseBody = await response.Content.ReadAsStringAsync();

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
                return new List<Section>();

            string cleanJson = rawJsonText.Trim();
            if (cleanJson.Contains("```json"))
            {
                cleanJson = cleanJson.Split("```json")[1].Split("```")[0].Trim();
            }
            else if (cleanJson.Contains("```"))
            {
                cleanJson = cleanJson.Split("```")[1].Split("```")[0].Trim();
            }

            return JsonSerializer.Deserialize<List<Section>>(cleanJson) ?? new List<Section>();
        }
        catch (Exception ex)
        {
            return new List<Section> { new Section { Title = "Exception", Content = ex.Message } };
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