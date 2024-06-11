using Application.Contracts.Services;
using Microsoft.Extensions.Logging;
using MathNet.Numerics.LinearAlgebra;

namespace Infrastructure.Services;

public class SemanticSearchService: ISemanticSearchService
{
    public IEmbeddingService _embeddingService;
    private readonly ILogger<SemanticSearchService> _logger;

    public SemanticSearchService(IEmbeddingService embeddingService, ILogger<SemanticSearchService> logger)
    {
        _embeddingService = embeddingService;
        _logger = logger;
    }

    public IEmbeddingService GetEmbeddingService(){
        return _embeddingService;
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
