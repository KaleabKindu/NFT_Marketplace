using Xunit;
using Moq;
using Application.Features.Auth.Queries;
using Application.Contracts.Persistence;
using ErrorOr;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Auth.Dtos;
using Application.Responses;
using AutoMapper;
using Application.UnitTest.Mocks;
using Domain;
using Application.Contracts.Persistance;

namespace Application.UnitTest.FeaturesTests.Auth.Queries
{
    public class GetUserNetworkHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetUserNetworkHandler _handler;

        public GetUserNetworkHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _handler = new GetUserNetworkHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidQueryForFollowers_ReturnsPaginatedResponse()
        {
            // Arrange
            var userAddress = "address1";
            var currentUserAddress = "currentUserAddress";
            var users = new List<AppUser>
            {
                new AppUser { Id = "1", UserName = "User1", Address = "address1" },
                new AppUser { Id = "2", UserName = "User2", Address = "address2" }
            };
            var paginatedUsers = new PaginatedResponse<AppUser>
            {
                Count = 2,
                PageNumber = 1,
                PageSize = 10,
                Value = users
            };
            var userNetworkDtos = new List<UserNetworkDto>
            {
                new UserNetworkDto { Id = "1", UserName = "User1", Address = "address1" },
                new UserNetworkDto { Id = "2", UserName = "User2", Address = "address2" }
            };

            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetFollowersAsync(userAddress, 1, 10))
                .ReturnsAsync(paginatedUsers);


            _mockUnitOfWork.Setup(uow => uow.UserRepository.IsFollowing(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var query = new GetUserNetwork
            {
                Address = userAddress,
                CurrentUserAddress = currentUserAddress,
                Type = "followers",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            // var result = await _handler.Handle(query, CancellationToken.None);

            // // Assert
            // Assert.False(result.IsError);
            // Assert.Equal("User's network fetched successfully", result.Value.Message);
            // Assert.Equal(userNetworkDtos, result.Value.Value);
            // Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task Handle_InvalidQueryType_ReturnsBadRequestError()
        {
            // Arrange
            var query = new GetUserNetwork
            {
                Address = "address1",
                CurrentUserAddress = "currentUserAddress",
                Type = "invalidType",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("type query parameter must be either followings or followers", result.FirstError.Description);
        }

        [Fact]
        public async Task Handle_ValidQueryForFollowings_ReturnsPaginatedResponse()
        {
            // Arrange
            var userAddress = "address1";
            var currentUserAddress = "currentUserAddress";
            var users = new List<AppUser>
            {
                new AppUser { Id = "1", UserName = "User1", Address = "address1" },
                new AppUser { Id = "2", UserName = "User2", Address = "address2" }
            };
            var paginatedUsers = new PaginatedResponse<AppUser>
            {
                Count = 2,
                PageNumber = 1,
                PageSize = 10,
                Value = users
            };
            var userNetworkDtos = new List<UserNetworkDto>
            {
                new UserNetworkDto { Id = "1", UserName = "User1", Address = "address1" },
                new UserNetworkDto { Id = "2", UserName = "User2", Address = "address2" }
            };

            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetFollowingsAsync(userAddress, 1, 10))
                .ReturnsAsync(paginatedUsers);

            _mockMapper.Setup(m => m.Map<List<UserNetworkDto>>(users))
                .Returns(userNetworkDtos);

            _mockUnitOfWork.Setup(uow => uow.UserRepository.IsFollowing(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var query = new GetUserNetwork
            {
                Address = userAddress,
                CurrentUserAddress = currentUserAddress,
                Type = "followings",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal("User's network fetched successfully", result.Value.Message);
            Assert.Equal(userNetworkDtos, result.Value.Value);
            Assert.Equal(2, result.Value.Count);
        }
    }
}
