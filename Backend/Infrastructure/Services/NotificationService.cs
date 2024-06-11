using Application.Contracts.Services;
using Application.Features.Notifications.Commands;
using Application.Features.Notifications.Dtos;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class NotificationService : INotificationService
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IMediator mediator, IHubContext<NotificationHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        public async Task SendNotification(CreateNotificationDto createNotificationDto)
        {
            var response = await _mediator.Send(new CreateNotificationCommand { CreateNotificationDto = createNotificationDto });

            if (!response.IsError)
            {
                await _hubContext.Clients.Group(createNotificationDto.UserId)
                .SendAsync("RecieveNotification", response.Value);
            }
        }
    }

}