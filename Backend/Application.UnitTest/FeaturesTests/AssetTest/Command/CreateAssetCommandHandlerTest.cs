using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Contracts.Services;
using Application.Features.Assets.Command;
using Application.Features.Assets.Dtos;
using Application.Profiles;
using Application.UnitTest.Mocks;

using AutoMapper;
using Domain.Assets;
using ErrorOr;
using Moq;

using Shouldly;

namespace ApplicationUnitTest.FeaturesTests.AssetTest.Command
{
    public class CreateAssetCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IAuctionManagementService> _mockAuctionManager;
        private readonly IMapper _mapper;
        private readonly CreateAssetCommandHandler _handler;
        private readonly CreateAssetDto _createAssetDto;

        public CreateAssetCommandHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _mockAuctionManager = MockUnitOfWork.GetAuctionManager();
            
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapper.CreateMapper();

            _handler = new CreateAssetCommandHandler(_mapper, _mockUnitOfWork.Object, _mockAuctionManager.Object);

            _createAssetDto = new CreateAssetDto
            {
                Name = "Digital Art Piece 1",
                TokenId = 1234567890,
                Description = "A unique piece of digital art.",
                Image = "https://example.com/images/art-piece1.png",
                Video = "https://example.com/videos/art-piece1.mp4",
                Audio = "https://example.com/audio/art-piece1.mp3",

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
                TransactionHash = "0x123456789abcdef",
            };
        }

        [Fact]
        public async Task CreateValidAsset()
        {
            var assets = await _mockUnitOfWork.Object.AssetRepository.GetAllAsync();
            int assetsCount = assets.Count();

            var result = await _handler.Handle(
                new CreateAssetCommand() { CreateAssetDto = _createAssetDto, Address = "address1" },
                CancellationToken.None
            );

            result.ShouldBeOfType<ErrorOr<BaseResponse<long>>>();

            assets = await _mockUnitOfWork.Object.AssetRepository.GetAllAsync();

            assets.Count().ShouldBe(assetsCount + 1);
        }

    }
}