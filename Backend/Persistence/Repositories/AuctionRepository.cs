using System;
using Application.Contracts.Persistence;
using Domain.Auctions;

namespace Persistence.Repositories
{
    public class AuctionRepository : Repository<Auction>, IAuctionRepository
    {
        public AuctionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}