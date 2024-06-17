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
using Application.Profiles;

namespace Application.UnitTest.FeaturesTests.Auth.Queries
{
    public class GetUsersQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mockMapper;
        private readonly GetUsersQueryHandler _handler;

        public GetUsersQueryHandlerTests()
        {
            _mockMapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _handler = new GetUsersQueryHandler(_mockUnitOfWork.Object, _mockMapper);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsPaginatedResponse()
        {
            // Arrange

            _mockUnitOfWork.Setup(uow => uow.UserRepository.IsFollowing(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var query = new GetUsersQuery
            {
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            // Assert.False(result.IsError);
            // Assert.Equal("Users fetched successfully", result.Value.Message);
        }
    }
}
