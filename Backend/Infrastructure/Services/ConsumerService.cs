using Application.Contracts.Services;
using Microsoft.Extensions.Hosting;
using Application.Events;

namespace Infrastructure.Services.Events;


public class ConsumerService: BackgroundService
{
    private readonly IEventHandler<ValueSetEvent> _event1Handler;
    private readonly RabbitMqService _messageQueue;

    public ConsumerService(
        IEventHandler<ValueSetEvent> event1Handler,
        RabbitMqService messageQueue
    ){
        _event1Handler = event1Handler;
        _messageQueue = messageQueue;
    }

    public async Task ProcessEventsAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Task>
        {
            ProcessEventsOfType<ValueSetEvent>(stoppingToken),
        };

        await Task.WhenAll(tasks);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ProcessEventsAsync(stoppingToken);
    }

    private async Task ProcessEventsOfType<TEvent>(CancellationToken stoppingToken) where TEvent : class
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Dequeue message from the message queue
            var eventData = _messageQueue.DequeueAsync<TEvent>();

            // Process the event in parallel using the appropriate handler
            await Task.Run(async () =>
            {
                switch (eventData)
                {
                    case ValueSetEvent ValueSetEvent:
                        await _event1Handler.HandleAsync(ValueSetEvent);
                        break;

                    default:
                        // Log or handle unexpected event type
                        break;
                }
            });
        }
    }
}
