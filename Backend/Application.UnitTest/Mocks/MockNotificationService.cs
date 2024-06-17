using Application.Contracts.Services;
using Application.Features.Notifications.Commands;
using Application.Features.Notifications.Dtos;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;


namespace Application.UnitTest.Mocks
{
    public class MockNotificationService : INotificationService
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<NotificationHub> _hubContext;

        public MockNotificationService(IMediator mediator, IHubContext<NotificationHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        public async Task SendNotification(CreateNotificationDto createNotificationDto)
        {
            // Mock the behavior of sending a notification
            var response = await _mediator.Send(new CreateNotificationCommand { CreateNotificationDto = createNotificationDto });

            if (!response.IsError)
            {
                await _hubContext.Clients.Group(createNotificationDto.UserId)
                    .SendAsync("RecieveNotification", response.Value);
            }
        }

        public async Task SendNotificationsForMultipleUsers(List<string> userIds, CreateNotificationDto createNotificationDto)
        {
            // Mock the behavior of sending notifications to multiple users
            var response = await _mediator.Send(new CreateMultipleNotificationsCommand { CreateNotificationDto = createNotificationDto, UserIds = userIds });

            if (!response.IsError)
            {
                foreach (var notification in response.Value)
                {
                    await _hubContext.Clients.Group(notification.ToId)
                        .SendAsync("RecieveNotification", notification);
                }
            }
        }

        public async Task NotifyAssetRefetch(long assetId)
        {
            // Mock the behavior of notifying clients to refetch an asset
            await _hubContext.Clients.All
                .SendAsync($"RefetchAsset{assetId}", assetId);
        }

        public async Task NotifyAssetProvenanceRefetch(long assetId)
        {
            // Mock the behavior of notifying clients to refetch an asset's provenance
            await _hubContext.Clients.All
                .SendAsync($"RefetchAssetProvenance{assetId}", assetId);
        }

        public async Task NotifyAssetBidsRefetch(long assetId)
        {
            // Mock the behavior of notifying clients to refetch an asset's bids
            await _hubContext.Clients.All
                .SendAsync($"RefetchAssetBids{assetId}", assetId);
        }

        public async Task NotifyRemoveAssetFromView(long assetId)
        {
            // Mock the behavior of notifying clients to remove an asset from the view
            await _hubContext.Clients.All
                .SendAsync($"RemoveAssetFromView{assetId}", assetId);
        }
    }
}