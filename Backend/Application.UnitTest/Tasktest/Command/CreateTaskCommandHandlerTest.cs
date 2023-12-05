using Application.Common.Exceptions;
using Application.Contracts;
using Application.Contracts.Presistence;
using Application.Features.Tasks.CQRS.Commands;
using Application.Features.Tasks.CQRS.Handlers;
using Application.Features.Tasks.Dtos;
using Application.Profiles;
using Application.UnitTest.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using System.Globalization;

namespace Application.UnitTest.Tasktest.Command
{
    public class CreateTaskCommandHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private readonly CreateTaskDto _taskDto;
        private readonly CreateTaskCommandHandler _handler;
        private readonly IUserAccessor _userAccessor;

        public CreateTaskCommandHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _userAccessor = MockUserAccessor.GetUserAccessor().Object;

            _taskDto = new CreateTaskDto
            {
                Title = "Title",
                Completed = false,
                StartDate = DateTime.ParseExact("20/02/2023 00:00", "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("22/02/2023 00:00", "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture),
                Description = "description",
            };

            _handler = new CreateTaskCommandHandler(_mockRepo.Object, _mapper, _userAccessor);

        }


        [Fact]
        public async Task CreateTask()
        {
            var result = await _handler.Handle(new CreateTaskCommand() { TaskDto = _taskDto }, CancellationToken.None);
            result.ShouldBeOfType<int>();

            var tasks = await _mockRepo.Object.TaskRepository.GetAllAsync();
            tasks.Count().ShouldBe(3);

        }

        [Fact]
        public async Task InvalidTask_Added()
        {

            _taskDto.StartDate = DateTime.ParseExact("23/02/2023 00:00", "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);

            await Should.ThrowAsync<ValidationException>
               (async () => await _handler.Handle(new CreateTaskCommand() { TaskDto = _taskDto }, CancellationToken.None));

            var tasks = await _mockRepo.Object.TaskRepository.GetAllAsync();
            tasks.Count().ShouldBe(2);

        }
    }
}




