using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class GroqService : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    // https://console.groq.com/authenticate 
    private const string ModelId = "llama-3.3-70b-versatile";
    private const string BaseUrl = "https://api.groq.com/openai/v1/chat/completions";

    public GroqService(HttpClient httpClient, string key)
    {
        _httpClient = httpClient;
        _apiKey = key;
    }

    public async Task<string> GetResponseAsync(List<ChatMessage> history)
    {
        var messages = history.Select(m => new
        {
            role = m.Role.ToLower() == "user" ? "user" : "assistant",
            content = m.Text
        }).ToArray();

        var requestBody = new
        {
            model = ModelId,
            messages = messages
        };

        try
        {
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"API Fehler ({response.StatusCode}): {responseBody}";
            }

            var result = JsonSerializer.Deserialize<GroqResponse>(responseBody);
            return result?.Choices?.FirstOrDefault()?.Message?.Content
                   ?? "Fehler: Antwort-Struktur ungültig.";
        }
        catch (Exception ex)
        {
            return $"Verbindungsfehler: {ex.Message}";
        }
    }

    public async Task<List<Section>> GetStructuredResponseAsync(List<ChatMessage> history)
    {
        string systemPrompt = "ANWEISUNG: Analysiere die Anfrage und strukturiere das Ergebnis als JSON-Array von Objekten mit den Feldern 'title' und 'content'.\n\n";

        var messages = new List<object>();
        for (int i = 0; i < history.Count; i++)
        {
            string text = history[i].Text;
            if (i == 0)
                text = systemPrompt + text;

            messages.Add(new
            {
                role = history[i].Role.ToLower() == "user" ? "user" : "assistant",
                content = text
            });
        }

        var requestBody = new
        {
            model = ModelId,
            messages = messages.ToArray(),
            response_format = new { type = "json_object" }
        };

        int maxRetries = 3;
        int delayMs = 1000;

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                var jsonRequest = JsonSerializer.Serialize(requestBody);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                request.Content = httpContent;

                var response = await _httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                if ((int)response.StatusCode == 429 && attempt < maxRetries)
                {
                    await Task.Delay(delayMs);
                    delayMs *= 2;
                    continue;
                }

                if (!response.IsSuccessStatusCode)
                {
                    return new List<Section> {
                        new Section { Title = $"API Fehler {(int)response.StatusCode}", Content = responseBody }
                    };
                }

                var result = JsonSerializer.Deserialize<GroqResponse>(responseBody);
                var rawJsonText = result?.Choices?.FirstOrDefault()?.Message?.Content;

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

    public class GroqResponse
    {
        [JsonPropertyName("choices")] public List<Choice>? Choices { get; set; }
    }
    public class Choice
    {
        [JsonPropertyName("message")] public Message? Message { get; set; }
    }
    public class Message
    {
        [JsonPropertyName("content")] public string? Content { get; set; }
    }

    public void Dispose()
    {
        try
        { }
        catch { }
    }
}