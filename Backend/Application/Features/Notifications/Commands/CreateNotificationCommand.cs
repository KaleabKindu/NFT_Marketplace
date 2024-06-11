using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Features.Notifications.Dtos;
using Domain.Notifications;
using Application.Common.Errors;

namespace Application.Features.Notifications.Commands
{
    public class CreateNotificationCommand : IRequest<ErrorOr<NotificationDto>>
    {
        public CreateNotificationDto CreateNotificationDto { get; set; }
    }

    public class CreateNotificationCommandHandler
        : IRequestHandler<CreateNotificationCommand, ErrorOr<NotificationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<NotificationDto>> Handle(
            CreateNotificationCommand command,
            CancellationToken cancellationToken
        )
        {
            var notificationData = _mapper.Map<Notification>(command.CreateNotificationDto);

            var notification = await _unitOfWork.NotificationRepository.AddAsync(notificationData);

            var changes = await _unitOfWork.SaveAsync();

            if (changes == 0)
                return ErrorFactory.InternalServerError("Notification", "Database Error: Unable To Save");

            return _mapper.Map<NotificationDto>(notification);
        }
    }
}
