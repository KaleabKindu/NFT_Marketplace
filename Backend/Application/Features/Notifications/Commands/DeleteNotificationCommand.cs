using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Common.Errors;

namespace Application.Features.Notifications.Commands
{
    public class DeleteNotificationCommand : IRequest<ErrorOr<Unit>>
    {
        public long NotificationId { get; set; }
    }

    public class DeleteNotificationCommandHandler
        : IRequestHandler<DeleteNotificationCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(
            DeleteNotificationCommand command,
            CancellationToken cancellationToken
        )
        {

            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(command.NotificationId);

            if (notification == null)
                return ErrorFactory.NotFound("Notification", "Notification Not Found");

            notification.IsDeleted = true;

            _unitOfWork.NotificationRepository.UpdateAsync(notification);

            var changes = await _unitOfWork.SaveAsync();

            if (changes == 0)
                return ErrorFactory.InternalServerError("Notification", "Database Error: Unable To Save");

            return Unit.Value;
        }
    }
}
