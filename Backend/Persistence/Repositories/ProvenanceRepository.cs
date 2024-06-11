using Application.Contracts.Persistence;
using Application.Responses;
using Domain.Provenances;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories;

public class ProvenanceRepository : Repository<Provenance>, IProvenanceRepository
{
    public ProvenanceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PaginatedResponse<Provenance>> GetAssetProvenance(long tokenId, int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;

        var provenances = _dbContext.Provenances
            .OrderBy(provenance => provenance.CreatedAt)
            .Include(provenance => provenance.From)
            .ThenInclude(frm => frm.Profile)
            .Include(provenance => provenance.To)
            .ThenInclude(to => to.Profile)
            .Include(provenance => provenance.Asset)
            .Where(provenance => provenance.Asset.TokenId == tokenId);

        var count = await provenances.CountAsync();

        provenances = provenances
            .Skip(skip)
            .Take(pageSize);

        return new PaginatedResponse<Provenance>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Count = count,
            Value = await provenances.ToListAsync(),
        };


    }
}