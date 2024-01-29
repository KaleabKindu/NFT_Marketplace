using System;
using Application.Contracts.Persistance;
using Domain.Auctions;

namespace Application.Contracts.Persistence
{
    public interface IAuctionRepository : IRepository<Auction>{}
    
}