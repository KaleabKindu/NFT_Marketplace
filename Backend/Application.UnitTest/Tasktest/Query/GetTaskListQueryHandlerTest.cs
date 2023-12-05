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
    public class GetTaskListQueryHandlerTest
    {


        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private readonly GetAllTaskQueryHandler _handler;
        public GetTaskListQueryHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetAllTaskQueryHandler(_mockRepo.Object, _mapper);

        }


        [Fact]
        public async Task GetTaskList()
        {
            var result = await _handler.Handle(new GetAllTaskQuery(), CancellationToken.None);
            result.ShouldBeOfType<List<TaskDto>>();
            result.Count().ShouldBe(2);
           
        }
    }
}



