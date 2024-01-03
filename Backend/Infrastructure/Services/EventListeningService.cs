using Nethereum.Web3;
using Nethereum.Contracts;
using Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Contracts.Services
{
    public class EventListeningService<TEvent> : BackgroundService where TEvent : IEventDTO, new()
    {
        private readonly Contract _contract;
        private readonly RabbitMqService _queueService;
        private readonly ILogger<EventListeningService<TEvent>> _logger;

        public EventListeningService(RabbitMqService queueService, IConfiguration configuration, ILogger<EventListeningService<TEvent>> logger)
        {
            _queueService = queueService;
            _logger = logger;

            var web3 = new Web3(configuration["SmartContract:RpcUrl"]);
            _contract = web3.Eth.GetContract(configuration["SmartContract:Abi"], configuration["SmartContract:Address"]);
        }

        public async Task Listen(CancellationToken stoppingToken)
        {
            var filterAll = await _contract.CreateFilterAsync();
            while (!stoppingToken.IsCancellationRequested)
            {
                var eventHandler = _contract.GetEvent<TEvent>();

                // var filterAll = await eventHandler.CreateFilterAsync();
                List<EventLog<TEvent>> events = await eventHandler.GetFilterChangesAsync(filterAll);
                if (events.Count > 0)
                {
                    _logger.LogInformation("Retrieved Event from Smart Contract.....");
                    // Dispatch events to the message queue
                    DispatchEventsToQueue(events.Select(evnt => evnt.Event).ToArray());
                }

                await Task.Delay(5000, stoppingToken); // Add a delay to avoid excessive API calls
            }
        }

        private void DispatchEventsToQueue(TEvent[] events)
        {
            foreach (TEvent evt in events)
            {
                _queueService.EnqueueAsync(evt, $"{typeof(TEvent).Name}Queue");
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
