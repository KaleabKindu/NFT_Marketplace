namespace Application.Contracts.Services;

public interface ISemanticSearchService
{
    public double EmbeddingSimilarity(float[] vectorA, float[] vectorB);
    public IEmbeddingService GetEmbeddingService();
}
