using Application.Contracts.Persistance;
using Application.Features.Bids.Dtos;
using AutoMapper;
using Domain.Bids;
using Microsoft.EntityFrameworkCore;
using ErrorOr;
namespace Persistence.Repositories
{
    public class BidRepository : Repository<Bid>, IBidRepository
    {
        private readonly IMapper _mapper;

        public BidRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<ErrorOr<Tuple<int, List<BidsListDto>>>> GetAllBidsAsync(int assetId, int page = 1, int limit = 10)
        {
            int skip = (page - 1) * limit;

            var bids = _dbContext.Bids
                .Where(x => x.AssetId == assetId)
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            var count = await bids.CountAsync();

            var bidsList = await bids
                .Skip(skip)
                .Take(limit)
                .Include(x => x.Asset)
                .Include(x => x.Bidder)
                .ThenInclude(x => x.Profile)
                .ToListAsync();

            return new Tuple<int, List<BidsListDto>>(count, _mapper.Map<List<BidsListDto>>(bidsList));
        }

        public async Task<int> Count(int AssetId)
        {
            return await _dbContext.Set<Bid>()
                .Where(entity => AssetId == default || entity.Asset.Id == AssetId)
                .CountAsync();
        }

        public async Task<Bid> GetBidByIdAsync(long id)
        {
            return await _dbContext.Set<Bid>()
                .Include(entity => entity.Bidder)
                .Include(entity => entity.Asset)
                .Where(entity => entity.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}