using Application.Contracts.Presistence;
using Application.Features.Tasks.CQRS.Handlers;
using Application.Features.Tasks.CQRS.Queries;
using Application.Features.Tasks.Dtos;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTest.Tasktest.Query
{
    public class GetTaskDetailQueryHandlerTest
    {


        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private int Id;
        private readonly GetTaskDetailQueryHandler _handler;
        public GetTaskDetailQueryHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            Id = 1;

            _handler = new GetTaskDetailQueryHandler(_mockRepo.Object, _mapper);

        }


        [Fact]
        public async Task GetTaskDetail()
        {
            var result = await _handler.Handle(new GetTaskDetailQuery() { Id = Id }, CancellationToken.None);
            result.ShouldBeOfType<TaskDto>();
        }

        [Fact]
        public async Task GetNonExistingTask()
        {
            Id = 0;
            var result = await _handler.Handle(new GetTaskDetailQuery() { Id = Id }, CancellationToken.None);
            result.ShouldBe(null);

        }
    }
}