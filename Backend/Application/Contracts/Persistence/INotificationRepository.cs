using Application.Contracts.Persistance;
using Application.Features.Notifications.Dtos;
using Application.Responses;
using Domain.Notifications;

namespace Application.Contracts.Persistence;

public interface INotificationRepository : IRepository<Notification>
{
    Task<PaginatedResponse<Notification>> GetNotifications(string userId, int pageNumber, int pageSize);
    Task<int> GetUnReadNotificationsCount(string userId);
    Task<List<Notification>> CreateMultipleNotifications(List<string> userIds, CreateNotificationDto createNotificationDto);
}