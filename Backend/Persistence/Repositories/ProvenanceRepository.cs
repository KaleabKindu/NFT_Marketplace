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

    public async Task<PaginatedResponse<Provenance>> GetAssetProvenance(long assetId, int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;

        var provenances = _dbContext.Provenances
            .Where(provenance => provenance.AssetId == assetId)
            .OrderBy(provenance => provenance.CreatedAt).AsQueryable();

        var count = await provenances.CountAsync();

        provenances = provenances
            .Skip(skip)
            .Take(pageSize)
            .Include(provenance => provenance.From)
            .ThenInclude(frm => frm.Profile)
            .Include(provenance => provenance.To)
            .ThenInclude(to => to.Profile);

        return new PaginatedResponse<Provenance>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Count = count,
            Value = await provenances.ToListAsync(),
        };


    }
}