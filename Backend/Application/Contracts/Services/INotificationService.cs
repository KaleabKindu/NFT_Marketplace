using Application.Features.Notifications.Dtos;

namespace Application.Contracts.Services
{
    public interface INotificationService
    {
        public Task SendNotification(CreateNotificationDto createNotificationDto);
        public Task SendNotificationsForMultipleUsers(List<string> userIds, CreateNotificationDto createNotificationDtos);
        public Task NotifyRemoveAssetFromView(long assetId);
        public Task NotifyAssetRefetch(long assetId);
        public Task NotifyAssetProvenanceRefetch(long assetId);
        public Task NotifyAssetBidsRefetch(long assetId);
    }
}
