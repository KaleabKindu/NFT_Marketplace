using System;
using Application.Contracts.Persistence;
using Domain.Auctions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AuctionRepository : Repository<Auction>, IAuctionRepository
    {
        public AuctionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Auction> GetByAuctionId(long auctionId)
        {
            return await _dbContext.Auctions
                .Where(auction => auction.AuctionId == auctionId)
                .FirstOrDefaultAsync();
        }
    }
}