using Application.Contracts.Persistance;
using Application.Contracts.Services;
using Application.Features.Assets.Command;
using Application.Features.Assets.Dtos;
using AutoMapper;
using Domain;
using ErrorOr;
using Moq;

namespace Application.Tests.Features.Assets.Command
{
    public class CreateAssetCommandHandlerTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IAuctionManagementService> _mockAuctionManager;
        private readonly Mock<INotificationService> _mockNotificationService;

        public CreateAssetCommandHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockAuctionManager = new Mock<IAuctionManagementService>();
            _mockNotificationService = new Mock<INotificationService>();
        }

        [Fact]
        public async Task Handle_WhenUserNotFound_ReturnsNotFound()
        {
            // Arrange
            var command = new CreateAssetCommand
            {
                CreateAssetDto = new CreateAssetDto(),
                Address = "some-address"
            };

            _mockUnitOfWork.Setup(x => x.UserRepository.GetUserByAddress(It.IsAny<string>()))
                .ReturnsAsync((AppUser)null);

            var handler = new CreateAssetCommandHandler(
                _mockMapper.Object,
                _mockUnitOfWork.Object,
                _mockAuctionManager.Object,
                _mockNotificationService.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.NotFound, result.FirstError.Type);
        }



    }
}
