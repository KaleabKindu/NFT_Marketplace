using Application.Contracts.Persistence;
using Application.Features.Provenances.Dtos;
using Application.Responses;
using Domain.Provenances;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories;

public class ProvenanceRepository : Repository<Provenance> , IProvenanceRepository
{
    public ProvenanceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PaginatedResponse<Provenance>> GetAssetProvenance(long tokenId, int pageNumber, int pageSize)
    {
        int skip = (pageSize - 1) * pageNumber;

        var provenances = _dbContext.Provenances
            .OrderBy(provenance => provenance.CreatedAt)
            .Include(provenance => provenance.From)
            .Include(provenance => provenance.To)
            .Include(provenance => provenance.Asset)
            .Where(provenance => provenance.Asset.TokenId == tokenId)
            .Skip<Provenance>(skip)
            .Take(pageSize);

        return new PaginatedResponse<Provenance>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Count = await provenances.CountAsync(),
            Value = await provenances.ToListAsync(),


        };


    }
}