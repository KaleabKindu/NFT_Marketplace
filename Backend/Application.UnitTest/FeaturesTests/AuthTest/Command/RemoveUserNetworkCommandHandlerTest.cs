using Xunit;
using Moq;
using Application.Features.Auth.Commands;
using Application.Contracts.Persistence;
using ErrorOr;
using Application.Common.Responses;
using System.Threading.Tasks;
using System.Threading;
using Application.Contracts.Persistance;
using Application.UnitTest.Mocks;
using MediatR;
using Application.Common.Exceptions;

namespace Application.UnitTest.FeaturesTests.AuthTest.Command
{
    public class RemoveUserNetworkCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly RemoveUserNetworkCommandHandler _handler;
        private readonly RemoveUserNetworkCommand _command;

        public RemoveUserNetworkCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _handler = new RemoveUserNetworkCommandHandler(_mockUnitOfWork.Object);
            _command = new RemoveUserNetworkCommand
            {
                Follower = "followerId",
                Followee = "followeeId"
            };
        }

        [Fact]
        public async Task Handle_UserNetworkRemovedSuccessfully_ReturnsSuccessResponse()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.UserRepository.RemoveNetwork(_command.Follower, _command.Followee))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal("User network removed successfully", result.Value.Message);
            Assert.Equal(Unit.Value, result.Value.Value);
        }

        [Fact]
        public async Task Handle_NoOutgoingNetworkExists_ReturnsErrorResponse()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.UserRepository.RemoveNetwork(_command.Follower, _command.Followee))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);
        
        }

        [Fact]
        public async Task Handle_SaveAsyncFails_ThrowsDbAccessException()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.UserRepository.RemoveNetwork(_command.Follower, _command.Followee))
                .ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            await Assert.ThrowsAsync<DbAccessException>(() => _handler.Handle(_command, CancellationToken.None));
        }
    }
}
