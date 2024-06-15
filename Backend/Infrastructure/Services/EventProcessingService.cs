using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Features.Bids.Dtos;
using Application.Features.Assets.Dtos;
using Application.Features.Auctions.Dtos;
using Application.Features.Assets.Commands;
using Application.Features.Auctions.Commands;
using Microsoft.Extensions.DependencyInjection;
using Application.Features.Assets.Command;

namespace Infrastructure.Services;

public class EventProcessingService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly RabbitMqService _messageQueue;
    private readonly ILogger<EventProcessingService> _logger;

    public EventProcessingService(
        IServiceScopeFactory serviceScopeFactory,
        RabbitMqService messageQueue,
        ILogger<EventProcessingService> logger
    )
    {
        _messageQueue = messageQueue;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() =>
        {
            HandleEvents(stoppingToken);
        },
            stoppingToken
        );
    }

    private void HandleEvents(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var auctionCreatedEvent = _messageQueue.DequeueAsync<AuctionCreatedEventDto>($"{typeof(AuctionCreatedEventDto)}");
            var bidPlacedEvent = _messageQueue.DequeueAsync<BidPlacedEventDto>($"{typeof(BidPlacedEventDto)}");
            var auctionEndedEvent = _messageQueue.DequeueAsync<AuctionEndedEventDto>($"{typeof(AuctionEndedEventDto)}");
            var assetSoldEvent = _messageQueue.DequeueAsync<AssetSoldEventDto>($"{typeof(AssetSoldEventDto)}");
            var resellAssetEvent = _messageQueue.DequeueAsync<ResellAssetEventDto>($"{typeof(ResellAssetEventDto)}");
            var transferAssetEvent = _messageQueue.DequeueAsync<TransferAssetEventDto>($"{typeof(TransferAssetEventDto)}");
            var deleteAssetEvent = _messageQueue.DequeueAsync<DeleteAssetEventDto>($"{typeof(DeleteAssetEventDto)}");
            var auctionCancelledEvent = _messageQueue.DequeueAsync<AuctionCancelledEventDto>($"{typeof(AuctionCancelledEventDto)}");

            if (auctionCreatedEvent != null)
            {
                Task.Run(
                    async () =>
                    {
                        var result = await mediator.Send(new CreateAuctionCommand { _event = auctionCreatedEvent }, stoppingToken);
                        _logger.LogInformation($"AuctionCreated Event Processing Result: {result.Value}");
                    },
                    stoppingToken
                );
            }

            if (bidPlacedEvent != null)
            {
                Task.Run(
                    async () =>
                    {
                        var result = await mediator.Send(new PlaceBidCommand { _event = bidPlacedEvent }, stoppingToken);
                        _logger.LogInformation($"BidPlaced Event Processing Result: {result.Value}");
                    },
                    stoppingToken
                );
            }

            if (auctionEndedEvent != null)
            {
                Task.Run(
                    async () =>
                    {
                        var result = await mediator.Send(new CloseAuctionCommand { _event = auctionEndedEvent }, stoppingToken);
                        _logger.LogInformation($"AuctionEnded Event Processing Result: {result.Value}");
                    },
                    stoppingToken
                );
            }

            if (assetSoldEvent != null)
            {
                Task.Run(
                    async () =>
                    {
                        var result = await mediator.Send(new SellAssetCommand { _event = assetSoldEvent }, stoppingToken);
                        _logger.LogInformation($"SellAsset Event Processing Result: {result.Value}");
                    },
                    stoppingToken
                );
            }

            if (resellAssetEvent != null)
            {
                Task.Run(
                    async () =>
                    {
                        var result = await mediator.Send(new ResellAssetCommand { _event = resellAssetEvent }, stoppingToken);
                        _logger.LogInformation($"ResellAsset Event Processing Result: {result.Value}");
                    },
                    stoppingToken
                );
            }

            if (transferAssetEvent != null)
            {
                Task.Run(
                    async () =>
                    {
                        var result = await mediator.Send(new TransferAssetCommand { _event = transferAssetEvent }, stoppingToken);
                        _logger.LogInformation($"TransferAsset Event Processing Result: {result.Value}");
                    },
                    stoppingToken
                );
            }

            if (deleteAssetEvent != null)
            {
                Task.Run(
                    async () =>
                    {
                        var result = await mediator.Send(new DeleteAssetCommand { _event = deleteAssetEvent }, stoppingToken);
                        _logger.LogInformation($"DeleteAsset Event Processing Result: {result.Value}");
                    },
                    stoppingToken
                );
            }

            if (auctionCancelledEvent != null)
            {
                Task.Run(
                    async () =>
                    {
                        var result = await mediator.Send(new CancelAuctionAssetCommand { _event = auctionCancelledEvent }, stoppingToken);
                        _logger.LogInformation($"CancelAuction Event Processing Result: {result.Value}");
                    },
                    stoppingToken
                );
            }
        }
    }
}
