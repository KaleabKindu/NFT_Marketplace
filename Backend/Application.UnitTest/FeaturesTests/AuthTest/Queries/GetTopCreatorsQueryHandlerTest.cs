using Xunit;
using Moq;
using Application.Features.Auth.Queries;
using Application.Contracts.Persistence;
using ErrorOr;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Auth.Dtos;
using AutoMapper;
using Application.UnitTest.Mocks;
using Domain;
using Application.Contracts.Persistance;

namespace Application.UnitTest.Features.Auth.Queries
{
    public class GetTopCreatorsQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetTopCreatorsQueryHandler _handler;

        public GetTopCreatorsQueryHandlerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _handler = new GetTopCreatorsQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsListOfTopCreators()
        {
            // Arrange
            var expectedTopCreators = new List<UserListDto>
            {
                new UserListDto { Id = "1", UserName = "User1" },
                new UserListDto { Id = "2", UserName = "User2" }
            };

            var topCreatorsFromRepo = new List<AppUser>
            {
                new AppUser { Id = "1", UserName = "User1" },
                new AppUser { Id = "2", UserName = "User2" }
            };

            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetTopCreators())
                .ReturnsAsync(topCreatorsFromRepo);

            _mockMapper.Setup(m => m.Map<List<UserListDto>>(topCreatorsFromRepo))
                .Returns(expectedTopCreators);

            // Act
            var result = await _handler.Handle(new GetTopCreatorsQuery(), CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal(expectedTopCreators, result.Value);
        }
    }
}
