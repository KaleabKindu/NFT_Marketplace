using Application.Features.Notifications.Dtos;

namespace Application.Contracts.Services
{
    public interface INotificationService
    {
        public Task SendNotification(CreateNotificationDto createNotificationDto);
        public Task SendNotificationsForMultipleUsers(List<string> userIds, CreateNotificationDto createNotificationDtos);
    }
}
