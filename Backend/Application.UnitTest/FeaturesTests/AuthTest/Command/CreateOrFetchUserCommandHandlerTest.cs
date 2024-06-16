using Xunit;
using Moq;
using Application.Features.Auth.Commands;
using Application.Contracts.Persistence;
using ErrorOr;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using Application.Contracts.Persistance;
using Domain;

namespace Application.UnitTest.FeaturesTests.AuthTest.Command

{
    public class CreateOrFetchUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateOrFetchUserCommandHandler _handler;
        private readonly CreateOrFetchUserCommand _command;

        public CreateOrFetchUserCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateOrFetchUserCommandHandler(_mockUnitOfWork.Object, _mockMapper.Object);
            _command = new CreateOrFetchUserCommand
            {
                Address = "testAddress"
            };
        }

        [Fact]
        public async Task Handle_UserCreatedSuccessfully_ReturnsSuccessResponse()
        {
            // Arrange
            var userDto = new UserDto { Id = "testId", Address = "testAddress" };
            var user = new AppUser { Id = "testId", Address = "testAddress" };
            _mockUnitOfWork.Setup(uow => uow.UserRepository.CreateOrFetchUserAsync(_command.Address))
                .ReturnsAsync(user);
            _mockMapper.Setup(mapper => mapper.Map<UserDto>(user))
                .Returns(userDto);

            // Act
            var result = await _handler.Handle(_command, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal("User created successfully", result.Value.Message);
            Assert.Equal(userDto, result.Value.Value);
        }
    }
}
