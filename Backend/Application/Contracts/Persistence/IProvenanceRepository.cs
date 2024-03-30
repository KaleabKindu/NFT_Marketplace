using Application.Contracts.Persistance;
using Application.Responses;
using Domain.Provenances;

namespace Application.Contracts.Persistence;

public interface IProvenanceRepository : IRepository<Provenance>
{
    Task<PaginatedResponse<Provenance>> GetAssetProvenance(long tokenId, int pageNumber, int pageSize);
}