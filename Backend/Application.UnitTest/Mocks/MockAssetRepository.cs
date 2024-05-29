using Application.Contracts.Presistence;
using Application.Features.Assets.Dtos;
using AutoMapper;
using Domain;
using Domain.Assets;
using ErrorOr;
using Moq;


namespace ApplicationUnitTest.Mocks
{
    public static class MockAssetRepository
    {
        public static Mock<IAssetRepository> GetAssetRepository(IMapper mapper)
        {
            var mockRepo = new Mock<IAssetRepository>();
            var _mapper = mapper;

            var assets = new List<Asset>
            {
                new Asset
                {
                    Name = "Digital Art Piece 1",
                    TokenId = 1234567890,
                    Description = "A unique piece of digital art.",
                    Image = "https://example.com/images/art-piece1.png",
                    Video = "https://example.com/videos/art-piece1.mp4",
                    Audio = "https://example.com/audio/art-piece1.mp3",
                    Owner = new AppUser { Id = "owner123", Address = "John Doe" },
                    OwnerId = "owner123",
                    Creator = new AppUser { Id = "creator123", Address = "Jane Smith" },
                    CreatorId = "creator123",
                    Category = AssetCategory.art,
                    Price = 250.0,
                    // Auction = new Auction { Id = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(7) },
                    // AuctionId = 1,
                    // Collection = new Collection { Id = 1, Name = "Digital Art Collection 1" },
                    // CollectionId = 1,
                    Royalty = 10.5f,
                    // Bids = new List<Bid>
                    // {
                    //     new Bid { Id = 1, Amount = 300.0, Bidder = new AppUser { Id = "bidder1", Name = "Alice" } },
                    //     new Bid { Id = 2, Amount = 350.0, Bidder = new AppUser { Id = "bidder2", Name = "Bob" } }
                    // },
                    Likes = 150,
                    TransactionHash = "0x123456789abcdef",
                    Status = AssetStatus.OnSale
                },
                new Asset
                {
                    Name = "Digital Art Piece 2",
                    TokenId = 1234567891,
                    Description = "Another unique piece of digital art.",
                    Image = "https://example.com/images/art-piece2.png",
                    Video = "https://example.com/videos/art-piece2.mp4",
                    Audio = "https://example.com/audio/art-piece2.mp3",
                    Owner = new AppUser { Id = "owner124", Address = "Mary Johnson" },
                    OwnerId = "owner124",
                    Creator = new AppUser { Id = "creator124", Address = "Steve Brown" },
                    CreatorId = "creator124",
                    Category = AssetCategory.art,
                    Price = 500.0,
                    // Auction = new Auction { Id = 2, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(5) },
                    // AuctionId = 2,
                    // Collection = new Collection { Id = 2, Name = "Digital Art Collection 2" },
                    // CollectionId = 2,
                    Royalty = 15.0f,
                    // Bids = new List<Bid>
                    // {
                    //     new Bid { Id = 3, Amount = 550.0, Bidder = new AppUser { Id = "bidder3", Name = "Charlie" } },
                    //     new Bid { Id = 4, Amount = 600.0, Bidder = new AppUser { Id = "bidder4", Name = "Dave" } }
                    // },
                    Likes = 200,
                    TransactionHash = "0xabcdef123456789",
                    Status = AssetStatus.OnSale
                },
                new Asset
                {
                    Name = "Digital Art Piece 3",
                    TokenId = 1234567892,
                    Description = "Yet another unique piece of digital art.",
                    Image = "https://example.com/images/art-piece3.png",
                    Video = "https://example.com/videos/art-piece3.mp4",
                    Audio = "https://example.com/audio/art-piece3.mp3",
                    Owner = new AppUser { Id = "owner125", Address = "Michael Williams" },
                    OwnerId = "owner125",
                    Creator = new AppUser { Id = "creator125", Address = "Laura Wilson" },
                    CreatorId = "creator125",
                    Category = AssetCategory.art,
                    Price = 750.0,
                    // Auction = new Auction { Id = 3, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(10) },
                    // AuctionId = 3,
                    // Collection = new Collection { Id = 3, Name = "Digital Art Collection 3" },
                    // CollectionId = 3,
                    Royalty = 12.0f,
                    // Bids = new List<Bid>
                    // {
                    //     new Bid { Id = 5, Amount = 800.0, Bidder = new AppUser { Id = "bidder5", Name = "Eve" } },
                    //     new Bid { Id = 6, Amount = 850.0, Bidder = new AppUser { Id = "bidder6", Name = "Frank" } }
                    // },
                    Likes = 250,
                    TransactionHash = "0x789abcdef123456",
                    Status = AssetStatus.Sold
                }
            };

            mockRepo.Setup(b => b.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(() =>
            {
                return assets;
            });


            mockRepo.Setup(b => b.AddAsync(It.IsAny<Asset>())).ReturnsAsync((Asset asset) =>
            {

                asset.Id = assets.Count + 1;
                assets.Add(asset);

                return asset;
            });

            mockRepo.Setup(b => b.UpdateAsync(It.IsAny<Asset>())).Callback((Asset asset) =>
            {

                var newAssets = assets.Where((b) => b.Id != asset.Id).ToList();
                newAssets.Add(asset);

                assets = newAssets;
            });

            mockRepo.Setup(b => b.DeleteAsync(It.IsAny<Asset>())).Callback((Asset asset) =>
            {
                assets.Remove(asset);
            });

            mockRepo.Setup(b => b.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((long id) =>
            {
                return assets.Find((b) => b.Id == id);
            });

            mockRepo.Setup(b => b.GetAssetWithDetail(It.IsAny<long>(), It.IsAny<string>())).ReturnsAsync((long id, string include) =>
            {
                var asset = assets.FirstOrDefault(a => a.Id == id);
                var assetDetailDto = _mapper.Map<AssetDetailDto>(asset);
                return ErrorOrFactory.From(assetDetailDto);
            });

            return mockRepo;
        }
    }
}