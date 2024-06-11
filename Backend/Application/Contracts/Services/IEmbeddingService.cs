namespace Application.Contracts.Services;

public interface IEmbeddingService
{
    public Task<float[][]> GetBatchEmbeddingAsync(List<string> texts);
}
