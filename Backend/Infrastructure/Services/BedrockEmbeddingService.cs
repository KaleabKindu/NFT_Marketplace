using Amazon;
using Amazon.Runtime;
using System.Text.Json;
using Amazon.BedrockRuntime;
using System.Text.Json.Nodes;
using Amazon.BedrockRuntime.Model;
using Microsoft.Extensions.Logging;
using Application.Contracts.Services;
using MathNet.Numerics.LinearAlgebra;

namespace Infrastructure.Services;

public class BedrockEmbeddingService: IEmbeddingService
{
    private readonly AmazonBedrockRuntimeClient _client;
    private readonly string _modelId;
    private readonly ILogger<BedrockEmbeddingService> _logger;

    public BedrockEmbeddingService(string region, string modelId, string accessKey, string secretKey, ILogger<BedrockEmbeddingService> logger)
    {
        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        var config = new AmazonBedrockRuntimeConfig
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(region)
        };
        _client = new AmazonBedrockRuntimeClient(credentials, config);
        _modelId = modelId;
        _logger = logger;
    }

    public async Task<float[][]> GetBatchEmbeddingAsync(List<string> texts)
    {
        _logger.LogInformation("Embedding................ "+texts.Count);
        var textsJson = new JsonArray();
        foreach (var text in texts)
            textsJson.Add(text);

        var nativeRequest = JsonSerializer.Serialize(new
        {
            texts = textsJson,
            input_type = "search_document"
        });

        var request = new InvokeModelRequest()
        {
            ModelId = _modelId,
            Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(nativeRequest)),
            ContentType = "application/json"
        };

        InvokeModelResponse response = await _client.InvokeModelAsync(request);
        JsonNode modelResponse = JsonNode.Parse(response.Body);

        var embeddingArray = modelResponse["embeddings"].AsArray();
        return embeddingArray?.Select(arrayNode => arrayNode?.AsArray().Select(node => (float)node!).ToArray()).ToArray();
    }

    public double EmbeddingSimilarity(float[] vectorA, float[] vectorB)
    {
        if (vectorA.Length != vectorB.Length)
            throw new ArgumentException("Vectors must be of the same dimension");

        var vecA = Vector<float>.Build.DenseOfArray(vectorA);
        var vecB = Vector<float>.Build.DenseOfArray(vectorB);

        var result = vecA.DotProduct(vecB) / (vecA.L2Norm() * vecB.L2Norm());
        _logger.LogInformation($"Similarity: {result}");
        return result;
    }
}
