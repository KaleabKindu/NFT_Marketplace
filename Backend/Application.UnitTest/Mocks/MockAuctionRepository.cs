using Moq;
using Application.Contracts.Persistence;
using Domain.Auctions;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistence.Repositories.Tests
{
    public static class MockAuctionRepository
    {
        // private readonly Mock<AppDbContext> _mockDbContext;
        // private readonly Mock<DbSet<Auction>> _mockAuctionDbSet;
        // private readonly List<Auction> _auctions;

        public static Mock<IAuctionRepository> GetMockAuctionRepository()
        {
            var _mockDbContext = new Mock<AppDbContext>();
            var _mockAuctionDbSet = new Mock<DbSet<Auction>>();
            var _mockRepo = new Mock<IAuctionRepository>();
            var _auctions = new List<Auction>
            {
                new Auction
                {
                    Id = 1,
                    AuctionId = 1,
                    TokenId = 1,
                    Seller = new AppUser { Id = "seller1" },
                    SellerId = "seller1",
                    FloorPrice = 100,
                    HighestBid = 50,
                    HighestBidder = new AppUser { Id = "bidder1" },
                    HighestBidderId = "bidder1",
                    AuctionEnd = DateTimeOffset.Now.AddDays(1).ToUnixTimeSeconds(),
                    JobId = "job1"
                },
                new Auction
                {
                    Id = 2,
                    AuctionId = 2,
                    TokenId = 2,
                    Seller = new AppUser { Id = "seller2" },
                    SellerId = "seller2",
                    FloorPrice = 200,
                    HighestBid = 150,
                    HighestBidder = new AppUser { Id = "bidder2" },
                    HighestBidderId = "bidder2",
                    AuctionEnd = DateTimeOffset.Now.AddDays(2).ToUnixTimeSeconds(),
                    JobId = "job2"
                },
                new Auction
                {
                    Id = 3,
                    AuctionId = 3,
                    TokenId = 3,
                    Seller = new AppUser { Id = "seller3" },
                    SellerId = "seller3",
                    FloorPrice = 300,
                    HighestBid = 250,
                    HighestBidder = new AppUser { Id = "bidder3" },
                    HighestBidderId = "bidder3",
                    AuctionEnd = DateTimeOffset.Now.AddDays(3).ToUnixTimeSeconds(),
                    JobId = "job3"
                }
            };

            // Set up the mock DbSet behavior
            // _mockDbContext.Setup(x => x.Auctions).Returns(_mockAuctionDbSet.Object);
            // _mockDbContext.Setup(r => r.Delete()).Returns(Task.CompletedTask);
            _mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((long auctionId) => _auctions.FirstOrDefault(a => a.AuctionId == auctionId));
            _mockRepo.Setup(x => x.AddAsync(It.IsAny<Auction>())).ReturnsAsync((Auction auction) =>
            {
                auction.AuctionId = _auctions.Count + 1;
                _auctions.Add(auction);
                return auction;
            });

            return _mockRepo;
        }

    }
}