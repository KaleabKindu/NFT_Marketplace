using Application.Contracts.Persistence;
using Application.Responses;
using Domain.Notifications;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories;

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<PaginatedResponse<Notification>> GetNotifications(string userId, int pageNumber, int pageSize)
    {

        int skip = (pageNumber - 1) * pageSize;

        var notifications = _dbContext.Notifications
            .OrderBy(not => not.IsRead)
            .ThenByDescending(Notification => Notification.CreatedAt)
            .Where(ntf => ntf.ToId == userId);

        var count = await notifications.CountAsync();


        notifications = notifications
                    .Skip(skip)
                    .Take(pageSize);

        return new PaginatedResponse<Notification>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Count = count,
            Value = await notifications.ToListAsync(),
        };
    }


    public async Task<int> GetUnReadNotificationsCount(string userId)
    {

        var unReadCount = await _dbContext.Notifications
            .Where(ntf => ntf.ToId == userId && !ntf.IsRead).CountAsync();

        return unReadCount;
    }
}