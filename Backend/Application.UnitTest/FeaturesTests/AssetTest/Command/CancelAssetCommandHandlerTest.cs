using Application.Contracts.Persistance;

using Application.Contracts.Services;
using Application.Features.Assets.Command;
using Application.Profiles;
using AutoMapper;
using Domain.Assets;
using Domain.Auctions;
using Moq;
using Xunit;

namespace Domain.Tests.Assets
{
    public class CancelAssetCommandHandlerTests
    {
        private readonly IMapper _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IAuctionManagementService> _mockAuctionManager;
        private readonly Mock<INotificationService> _mockNotificationService;


        public CancelAssetCommandHandlerTests()
        {
            _mockMapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockAuctionManager = new Mock<IAuctionManagementService>();
            _mockNotificationService = new Mock<INotificationService>();

        }

        [Fact]
        public async Task Handle_CancelsAssetAndAuction_WhenAssetHasAuction()
        {

            var handler = new CancelAssetCommandHandler(_mockUnitOfWork.Object, _mockNotificationService.Object);

            var asset = new Asset
            {
                Id = 1,
                Auction = new Auction { Id = 1 },
                AuctionId = 1,
                Status = AssetStatus.OnAuction
            };


            var command = new CancelAssetCommand { Id = 1 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);

            Assert.Equal(AssetStatus.NotOnSale, asset.Status);
        }

        [Fact]
        public async Task Handle_CancelsAsset_WhenAssetDoesNotHaveAuction()
        {

            var handler = new CancelAssetCommandHandler(_mockUnitOfWork.Object, _mockNotificationService.Object);

            var asset = new Asset
            {
                Id = 1,
                Auction = null,
                AuctionId = null,
                Status = AssetStatus.OnFixedSale
            };

            var command = new CancelAssetCommand { Id = 1 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.IsError);
            Assert.Equal(AssetStatus.NotOnSale, asset.Status);
        }
    }
}