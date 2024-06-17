using Xunit;
using Moq;
using Application.Features.Auth.Commands;
using Application.Contracts.Persistence;
using ErrorOr;
using Application.Common.Responses;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Application.Contracts.Persistance;
using Application.Common.Exceptions;

namespace Application.UnitTest.FeaturesTests.AuthTest.Command
{
    public class CreateUserNetworkCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CreateUserNetworkCommandHandler _handler;
        private readonly CreateUserNetworkCommand _command;

        public CreateUserNetworkCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new CreateUserNetworkCommandHandler(_mockUnitOfWork.Object);
            _command = new CreateUserNetworkCommand
            {
                Follower = "testFollower",
                Followee = "testFollowee"
            };
        }

        [Fact]
        public async Task Handle_NetworkCreatedSuccessfully_ReturnsSuccessResponse()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.UserRepository.CreateNetwork(_command.Follower, _command.Followee))
                .ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal("User network created successfully", result.Value.Message);
            Assert.Equal(Unit.Value, result.Value.Value);
        }

        [Fact]
        public async Task Handle_NetworkAlreadyExists_ReturnsErrorResponse()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.UserRepository.CreateNetwork(_command.Follower, _command.Followee))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);
   
        }

        [Fact]
        public async Task Handle_SaveAsyncReturnsZero_ThrowsDbAccessException()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.UserRepository.CreateNetwork(_command.Follower, _command.Followee))
                .ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act and Assert
            await Assert.ThrowsAsync<DbAccessException>(() => _handler.Handle(_command, CancellationToken.None));
        }
    }
}
