using ErrorOr;
using MediatR;
using Application.Contracts.Persistance;
using Application.Common.Errors;

namespace Application.Features.Notifications.Commands
{
    public class MarkNotificationAsReadCommand : IRequest<ErrorOr<bool>>
    {
        public long NotificationId { get; set; }
    }

    public class MarkNotificationAsReadCommandHandler
        : IRequestHandler<MarkNotificationAsReadCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationAsReadCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(
            MarkNotificationAsReadCommand command,
            CancellationToken cancellationToken
        )
        {
            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(command.NotificationId);

            if (notification == null)
                return ErrorFactory.NotFound("Notification", "Notification Not Found");

            notification.IsRead = true;

            _unitOfWork.NotificationRepository.UpdateAsync(notification);

            var changes = await _unitOfWork.SaveAsync();

            return changes == 0;
        }
    }
}
