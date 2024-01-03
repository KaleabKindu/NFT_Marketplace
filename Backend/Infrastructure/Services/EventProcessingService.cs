using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Contracts.Services;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Infrastructure.Services.Events;

public class EventProcessingService<TEvent> : BackgroundService where TEvent : IEventDTO
{
    private readonly IEventHandler<TEvent> _eventHandler;
    private readonly RabbitMqService _messageQueue;
    private readonly ILogger<EventProcessingService<TEvent>> _logger;

    public EventProcessingService(
        IEventHandler<TEvent> eventHandler,
        RabbitMqService messageQueue,
        ILogger<EventProcessingService<TEvent>> logger
    ){
        _eventHandler = eventHandler;
        _messageQueue = messageQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(() => {
                ProcessEvents(stoppingToken);
            },
            stoppingToken
        );
    }

    private void ProcessEvents(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Dequeue message from the message queue
            var eventData = _messageQueue.DequeueAsync<TEvent>( $"{typeof(TEvent).Name}Queue");

            // Process the event in parallel using the appropriate handler
            if(eventData != null){
                Task.Run(
                    async () =>
                    {
                        await _eventHandler.HandleAsync(eventData);
                    },
                    stoppingToken
                );
            }
        }
    }
}
