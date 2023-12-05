using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using Application.UnitTest.Mocks;
using Application.Contracts.Presistence;
using Application.Features.Tasks.CQRS.Handlers;
using Application.Features.Tasks.Dtos;
using Application.Features.Tasks.CQRS.Commands;
using Application.Common.Exceptions;
using System.Globalization;
using Application.Profiles;

namespace Application.UnitTest.Tasktest.Command
{
    public class DeleteTaskCommandHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private int _id { get; set; }
        private readonly DeleteTaskCommandHandler _handler;
        private readonly CreateTaskDto _taskDto;
        public DeleteTaskCommandHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _id = 2;

            _handler = new DeleteTaskCommandHandler(_mockRepo.Object, _mapper);

        }


        [Fact]
        public async Task DeleteTask()
        {

            var result = await _handler.Handle(new DeleteTaskCommand() { Id = _id }, CancellationToken.None);
            result.ShouldBeOfType<Unit>();

            var Tasks = await _mockRepo.Object.TaskRepository.GetAllAsync();
            Tasks.Count().ShouldBe(1);
        }

        [Fact]
        public async Task Delete_Task_Doesnt_Exist()
        {

            _id = 0;
            await Should.ThrowAsync<NotFoundException>
              (async () => await _handler.Handle(new DeleteTaskCommand() { Id = _id }, CancellationToken.None));

            var tasks = await _mockRepo.Object.TaskRepository.GetAllAsync();
            tasks.Count().ShouldBe(2);

        }
    }
}



