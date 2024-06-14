using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Features.Notifications.Dtos;
using Application.Common.Errors;

namespace Application.Features.Notifications.Commands
{
    public class CreateMultipleNotificationsCommand : IRequest<ErrorOr<List<NotificationDto>>>
    {
        public List<string> UserIds { get; set; }
        public CreateNotificationDto CreateNotificationDto { get; set; }
    }

    public class CreateMultipleNotificationsCommandHandler
        : IRequestHandler<CreateMultipleNotificationsCommand, ErrorOr<List<NotificationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMultipleNotificationsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<NotificationDto>>> Handle(
            CreateMultipleNotificationsCommand command,
            CancellationToken cancellationToken
        )
        {


            var notifications = await _unitOfWork.NotificationRepository.CreateMultipleNotifications(command.UserIds, command.CreateNotificationDto);

            var changes = await _unitOfWork.SaveAsync();

            if (changes == 0)
                return ErrorFactory.InternalServerError("Notification", "Database Error: Unable To Save");

            return _mapper.Map<List<NotificationDto>>(notifications);
        }
    }
}
