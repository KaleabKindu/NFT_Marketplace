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

        public async Task SendNotificationsForMultipleUsers(List<string> userIds, CreateNotificationDto createNotificationDto)
        {

            var response = await _mediator.Send(new CreateMultipleNotificationsCommand { CreateNotificationDto = createNotificationDto, UserIds = userIds });

            if (!response.IsError)
            {
                foreach (var notifiction in response.Value)
                {
                    await _hubContext.Clients.Group(notifiction.ToId)
                    .SendAsync("RecieveNotification", notifiction);
                }
            }
        }

        public async Task NotifyAssetRefetch(long assetId)
        {

            await _hubContext.Clients.All
            .SendAsync($"RefetchAsset{assetId}", assetId);

        }
        public async Task NotifyAssetProvenanceRefetch(long assetId)
        {
            await _hubContext.Clients.All
           .SendAsync($"RefetchAssetProvenance{assetId}", assetId);

        }
        public async Task NotifyAssetBidsRefetch(long assetId)
        {
            await _hubContext.Clients.All
            .SendAsync($"RefetchAssetBids{assetId}", assetId);
        }

        public async Task NotifyRemoveAssetFromView(long assetId)
        {
            await _hubContext.Clients.All
            .SendAsync($"RemoveAssetFromView{assetId}", assetId);
        }
    }
}
