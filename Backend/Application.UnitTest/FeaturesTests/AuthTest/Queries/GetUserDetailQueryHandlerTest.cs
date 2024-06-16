using Xunit;
using Moq;
using Application.Features.Auth.Queries;
using Application.Contracts.Persistence;
using ErrorOr;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Auth.Dtos;
using AutoMapper;
using Application.UnitTest.Mocks;
using Application.Common.Responses;
using Domain;
using Application.Contracts.Persistance;

namespace Application.UnitTest.FeaturesTests.Auth.Queries
{
    public class GetUserDetailQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetUserDetailQueryHandler _handler;

        public GetUserDetailQueryHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _handler = new GetUserDetailQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_UserExists_ReturnsUserDetail()
        {
            // Arrange
            var userAddress = "address1";
            var userFromRepo = new AppUser { Id = "1", UserName = "User1", Address = userAddress };
            var expectedUserDetail = new UserDetailDto { Id = "1", UserName = "User1" };

            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetUserByAddress(userAddress))
                .ReturnsAsync(userFromRepo);

            _mockMapper.Setup(m => m.Map<UserDetailDto>(userFromRepo))
                .Returns(expectedUserDetail);

            var query = new GetUserDetailQuery { address = userAddress };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal("Bid details fetched successfully", result.Value.Message);
            Assert.Equal(expectedUserDetail, result.Value.Value);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_ReturnsNotFoundError()
        {
            // Arrange
            var userAddress = "non_existent_address";

            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetUserByAddress(userAddress))
                .ReturnsAsync((AppUser)null);

            var query = new GetUserDetailQuery { address = userAddress };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("User not found", result.FirstError.Description);
        }
    }
}
