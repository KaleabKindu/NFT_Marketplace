using System.Text.Json;
using System.Text.Json.Nodes;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Application.Contracts.Services;

namespace Infrastructure.Services;

public class OpenAiEmbeddingService: IEmbeddingService
{
    private readonly ILogger<OpenAiEmbeddingService> _logger;

    private static readonly HttpClient client = new HttpClient();
    private readonly string _apiKey;
    private readonly string _model;


    public OpenAiEmbeddingService(string OpenAiApiKey, string model){
        _apiKey = OpenAiApiKey;
        _model = model;
    }

    public async Task<float[][]> GetBatchEmbeddingAsync(List<string> texts)
    {
        var request = new
        {
            model = _model,
            input = texts
        };

        var jsonRequest = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonRequest);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await client.PostAsync("https://api.openai.com/v1/embeddings", content);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var embeddingResponse = JsonNode.Parse(responseBody);

        var embeddings = embeddingResponse["data"].AsArray();
        return embeddings?.Select(arrayNode => arrayNode["embedding"].AsArray().Select(node => (float)node!).ToArray()).ToArray();
    }
}