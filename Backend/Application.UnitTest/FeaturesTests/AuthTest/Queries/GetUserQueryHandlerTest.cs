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

namespace Application.UnitTest.Features.Auth.Queries
{
    public class GetUsersQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetUsersQueryHandler _handler;

        public GetUsersQueryHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _handler = new GetUsersQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsPaginatedResponse()
        {
            // Arrange
            var currentAddress = "currentAddress";
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
            var userDtos = new List<UserListDto>
            {
                new UserListDto { Id = "1", UserName = "User1", Address = "address1" },
                new UserListDto { Id = "2", UserName = "User2", Address = "address2" }
            };

            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetAllUsersAsync(1, 10, currentAddress))
                .ReturnsAsync(paginatedUsers);

            _mockMapper.Setup(m => m.Map<List<UserListDto>>(users))
                .Returns(userDtos);

            _mockUnitOfWork.Setup(uow => uow.UserRepository.IsFollowing(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var query = new GetUsersQuery
            {
                CurrentAddress = currentAddress,
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal("Users fetched successfully", result.Value.Message);
            Assert.Equal(userDtos, result.Value.Value);
            Assert.Equal(2, result.Value.Count);
        }
    }
}
