using Xunit;
using Moq;
using Application.Features.Auth.Commands;
using Application.Contracts.Persistence;
using ErrorOr;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Application.UnitTest.Mocks;
using ApplicationUnitTest.Mocks;
using Application.Contracts.Persistance;

namespace Application.UnitTest.FeaturesTests.AuthTest.Command
{
public class AuthenticateUserCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly AuthenticateUserCommandHandler _handler;
    private readonly AuthenticateUserCommand _command;

    public AuthenticateUserCommandHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        // Use the mock repository
        var mockUserRepository = MockAppUserRepository.GetAppUserRepository();
        _mockUnitOfWork.Setup(uow => uow.UserRepository).Returns(mockUserRepository.Object);

        _handler = new AuthenticateUserCommandHandler(_mockUnitOfWork.Object);
        _command = new AuthenticateUserCommand
        {
            Address = "address1",
            SignedNonce = "testSignedNonce"
        };
    }

    [Fact]
    public async Task Handle_UserAuthenticated_ReturnsSuccessResponse()
    {
        // Arrange
        var tokenDto = new TokenDto { Id = "testId", AccessToken = "testAccessToken", ExpireInDays = 1 };
        var successResult = (ErrorOr<TokenDto>)tokenDto;

        _mockUnitOfWork.Setup(uow => uow.UserRepository.AuthenticateUserAsync(_command.Address, _command.SignedNonce))
            .ReturnsAsync(successResult);

        // Act
        var result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal("User authenticated successfully", result.Value.Message);
        Assert.Equal(tokenDto.AccessToken, result.Value.Value.AccessToken);
    }

    [Fact]
    public async Task Handle_AuthenticationFails_ReturnsErrorResponse()
    {
        // Arrange
        var errors = new List<Error> { Error.Failure("Authentication Failed") };
        var errorResult = ErrorOr<TokenDto>.From(errors);

        _mockUnitOfWork.Setup(uow => uow.UserRepository.AuthenticateUserAsync(_command.Address, _command.SignedNonce))
            .ReturnsAsync(errorResult);

        // Act
        var result = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(errors[0], result.Errors);
    }
}
}