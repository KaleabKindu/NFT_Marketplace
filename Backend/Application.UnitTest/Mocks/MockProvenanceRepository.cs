using Application.Contracts.Persistence;
using AutoMapper;
using Domain;
using Domain.Assets;
using Domain.Provenances;
using ErrorOr;
using Moq;


namespace ApplicationUnitTest.Mocks
{
    public static class MockProvenanceRepository
    {
        public static Mock<IProvenanceRepository> GetProvenanceRepository()
        {
            var mockRepo = new Mock<IProvenanceRepository>();

            var provenances = new List<Provenance>()
             {
                new Provenance
                {
                    Asset = new Asset
                    {
                        Name = "Digital Art Piece 1",
                        TokenId = 1234567890,
                        Description = "A unique piece of digital art.",
                        Image = "https://example.com/images/art-piece1.png",
                        Owner = new AppUser { Id = "owner123", Address ="address1" },
                        OwnerId = "owner123",
                        Creator = new AppUser { Id = "creator123", Address = "address1" },
                        CreatorId = "creator123",
                        Category = AssetCategory.art,
                        Price = 250.0,
                        Royalty = 10.5f,
                        Likes = 150,
                        TransactionHash = "0x123456789abcdef",
                        Status = AssetStatus.OnSale
                    },
                    AssetId = 1,
                    Event = Event.Mint,
                    From = new AppUser { Id = "user1", Address = "address1" },
                    FromId = "user1",
                    Price = 300.0,
                    TransactionHash = "0xabcdef1234567890"
                },
                new Provenance
                {
                    Asset = new Asset
                    {
                        Name = "Digital Art Piece 2",
                        TokenId = 1234567891,
                        Description = "Another unique piece of digital art.",
                        Image = "https://example.com/images/art-piece2.png",
                        Owner = new AppUser { Id = "owner124", Address = "Mary Johnson" },
                        OwnerId = "owner124",
                        Creator = new AppUser { Id = "creator124", Address = "Steve Brown" },
                        CreatorId = "creator124",
                        Category = AssetCategory.art,
                        Price = 500.0,
                        Royalty = 15.0f,
                        Likes = 200,
                        TransactionHash = "0xabcdef1234567891",
                        Status = AssetStatus.OnSale
                    },
                    AssetId = 2,
                    Event = Event.Sale,
                    From = new AppUser { Id = "user3", Address = "Charlie Davis" },
                    FromId = "user3",
                    To = new AppUser { Id = "user4", Address = "Diana Miller" },
                    ToId = "user4",
                    Price = 550.0,
                    TransactionHash = "0xabcdef1234567892"
                },
                new Provenance
                {
                    Asset = new Asset
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
                        Royalty = 12.0f,
                        Likes = 250,
                        TransactionHash = "0x789abcdef123456",
                        Status = AssetStatus.Sold
                    },
                    AssetId = 3,
                    Event = Event.Sale,
                    From = new AppUser { Id = "user5", Address = "address1" },
                    FromId = "user5",
                    To = new AppUser { Id = "user6", Address = "address2" },
                    ToId = "user6",
                    Price = 800.0,
                    TransactionHash = "0xabcdef1234567893"
                }
            };


            mockRepo.Setup(b => b.AddAsync(It.IsAny<Provenance>())).ReturnsAsync((Provenance provenance) =>
            {
                provenances.Add(provenance);
                return provenance;
            });

            return mockRepo;
        }
    }
}