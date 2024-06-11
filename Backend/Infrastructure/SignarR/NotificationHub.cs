
using System.Security.Claims;
using Application.Contracts.Services;
using Application.Features.Notifications.Commands;
using Application.Features.Notifications.Dtos;
using Application.Features.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services
{
    public class NotificationHub : Hub
    {
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;

        public NotificationHub(INotificationService notificationService, IMediator mediator)
        {
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task DeleteNotification(long notificationId)
        {
            await _mediator.Send(new DeleteNotificationCommand { NotificationId = notificationId });
        }

        public async Task MarkAsRead(long notificationId)
        {
            await _mediator.Send(new MarkNotificationAsReadCommand { NotificationId = notificationId });
        }

        public async Task SendNotification(CreateNotificationDto createNotificationDto)
        {
            await _notificationService.SendNotification(createNotificationDto);
        }

        public async Task GetNotifications(int pageNumber, int pageSize)
        {
            await LoadNotifications(false, pageNumber, pageSize);
        }

        public override async Task OnConnectedAsync()
        {

            await LoadNotifications(true);

        }

        private async Task LoadNotifications(bool start, int pageNumber = 1, int pageSize = 10)
        {
            var context = Context.GetHttpContext();

            var user = context.User;

            if (user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirstValue(ClaimTypes.PrimarySid);

                if (start) await Groups.AddToGroupAsync(Context.ConnectionId, userId);

                var result = await _mediator.Send(new GetNotificationsQuery { UserId = userId, PageNumber = pageNumber, PageSize = pageSize });
                if (!result.IsError)
                    await Clients.Caller.SendAsync("LoadNotifications", result.Value);


                var unReadCount = await _mediator.Send(new GetUnreadNotificationsCountQuery { UserId = userId });

                if (!unReadCount.IsError)
                    await Clients.Caller.SendAsync("UnReadNotificationCount", unReadCount.Value);
            }
            else
            {
                await Clients.Caller.SendAsync("LoadNotifications", "User is not authenticated.");
            }
        }
    }

}