using Nethereum.Web3;
using Nethereum.Contracts;
using Infrastructure.Services;
using Nethereum.Web3.Accounts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Features.Bids.Dtos;
using Application.Features.Assets.Dtos;
using Application.Features.Auctions.Dtos;
using Microsoft.Extensions.Configuration;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Contracts.Services
{
    public class EventListeningService : BackgroundService
    {
        private readonly Contract _contract;
        private readonly RabbitMqService _queueService;
        private readonly ILogger<EventListeningService> _logger;

        public EventListeningService(RabbitMqService queueService, IConfiguration configuration, ILogger<EventListeningService> logger)
        {
            _queueService = queueService;
            _logger = logger;
            var account = new Account(configuration["SmartContract:PrivateKey"]);
            var web3 = new Web3(account, configuration["SmartContract:RpcUrl"]);
            _contract = web3.Eth.GetContract(configuration["SmartContract:Abi"], configuration["SmartContract:Address"]);
        }

        public async Task Listen(CancellationToken stoppingToken)
        {
            var auctionCreatedEventHandler = _contract.GetEvent<AuctionCreatedEventDto>();
            var auctionCreatedFilter = await auctionCreatedEventHandler.CreateFilterAsync();
            var bidPlacedEventHandler = _contract.GetEvent<BidPlacedEventDto>();
            var bidPlacedFilter = await bidPlacedEventHandler.CreateFilterAsync();
            var auctionEndedEventHandler = _contract.GetEvent<AuctionEndedEventDto>();
            var auctionEndedFilter = await auctionEndedEventHandler.CreateFilterAsync();
            var assetSoldEventHandler = _contract.GetEvent<AssetSoldEventDto>();
            var assetSoldFilter = await assetSoldEventHandler.CreateFilterAsync();
            var resellAssetEventHandler = _contract.GetEvent<ResellAssetEventDto>();
            var resellAssetFilter = await resellAssetEventHandler.CreateFilterAsync();
            var transferAssetEventHandler = _contract.GetEvent<TransferAssetEventDto>();
            var transferAssetFilter = await transferAssetEventHandler.CreateFilterAsync();
            var deleteAssetEventHandler = _contract.GetEvent<DeleteAssetEventDto>();
            var deleteAssetFilter = await deleteAssetEventHandler.CreateFilterAsync();

            _logger.LogInformation("Listening for SmartContract Events...");
            while (!stoppingToken.IsCancellationRequested)
            {
                // AuctionCreated Event
                var auctionCreatedEvents = await auctionCreatedEventHandler.GetAllChangesAsync(auctionCreatedFilter);
                if (auctionCreatedEvents.Count > 0)
                {
                    _logger.LogInformation($"Retrieved AuctionCreated Events...");
                    // Dispatch events to the message queue
                    DispatchEventsToQueue(auctionCreatedEvents.Select(evnt => new AuctionCreatedEventDto
                    {
                        AuctionId = evnt.Event.AuctionId,
                        TokenId = evnt.Event.TokenId,
                        Seller = evnt.Event.Seller,
                        FloorPrice = evnt.Event.FloorPrice,
                        AuctionEnd = evnt.Event.AuctionEnd,
                        TransactionHash = evnt.Log.TransactionHash,
                    }).ToArray());
                }

                // BidPlaced Event
                var bidPlacedEvents = await bidPlacedEventHandler.GetAllChangesAsync(bidPlacedFilter);
                if (bidPlacedEvents.Count > 0)
                {
                    _logger.LogInformation($"Retrieved BidPlaced Events...");
                    // Dispatch events to the message queue
                    DispatchEventsToQueue(bidPlacedEvents.Select(evnt => new BidPlacedEventDto
                    {
                        AuctionId = evnt.Event.AuctionId,
                        Bidder = evnt.Event.Bidder,
                        Amount = evnt.Event.Amount,
                        TransactionHash = evnt.Log.TransactionHash,
                    }).ToArray());
                }

                // AuctionEnded Event
                var auctionEndedEvents = await auctionEndedEventHandler.GetAllChangesAsync(auctionEndedFilter);
                if (auctionEndedEvents.Count > 0)
                {
                    _logger.LogInformation($"Retrieved AuctionEnded Events...");
                    DispatchEventsToQueue(auctionEndedEvents.Select(evnt => new AuctionEndedEventDto
                    {
                        AuctionId = evnt.Event.AuctionId,
                        Winner = evnt.Event.Winner,
                        TransactionHash = evnt.Log.TransactionHash,
                    }).ToArray());
                }

                // AssetSold Event
                var assetSoldEvents = await assetSoldEventHandler.GetAllChangesAsync(assetSoldFilter);
                if (assetSoldEvents.Count > 0)
                {
                    _logger.LogInformation("Retrieved AssetSold Events...");
                    DispatchEventsToQueue(assetSoldEvents.Select(evnt => new AssetSoldEventDto
                    {
                        TokenId = evnt.Event.TokenId,
                        To = evnt.Event.To,
                        TransactionHash = evnt.Log.TransactionHash,
                    }).ToArray());
                }

                // ResellAsset Event
                var resellAssetEvents = await resellAssetEventHandler.GetAllChangesAsync(resellAssetFilter);
                if (resellAssetEvents.Count > 0)
                {
                    _logger.LogInformation("Retrieved ResellAsset Events...");
                    DispatchEventsToQueue(resellAssetEvents.Select(evnt => evnt.Event).ToArray());
                }

                // TransferAsset Event
                var transferAssetEvents = await transferAssetEventHandler.GetAllChangesAsync(transferAssetFilter);
                if (transferAssetEvents.Count > 0)
                {
                    _logger.LogInformation("Retrieved TransferAsset Events...");
                    DispatchEventsToQueue(transferAssetEvents.Select(evnt => new TransferAssetEventDto
                    {
                        TokenId = evnt.Event.TokenId,
                        NewOwner = evnt.Event.NewOwner,
                        TransactionHash = evnt.Log.TransactionHash,
                    }).ToArray());
                }

                // DeleteAsset Event
                var deleteAssetEvents = await deleteAssetEventHandler.GetAllChangesAsync(deleteAssetFilter);
                if (deleteAssetEvents.Count > 0)
                {
                    _logger.LogInformation("Retrieved DeleteAsset Events...");
                    DispatchEventsToQueue(deleteAssetEvents.Select(evnt => evnt.Event).ToArray());
                }

                await Task.Delay(10000, stoppingToken); // Add a delay to avoid excessive API calls
            }
        }

        private void DispatchEventsToQueue(IEventDTO[] events)
        {
            foreach (IEventDTO evt in events)
            {
                _queueService.EnqueueAsync(evt, $"{evt}");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Listen(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _queueService.Close();
            await base.StopAsync(cancellationToken);
        }
    }
}
