using Application.Contracts.Persistance;
using Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Events.Handlers;


public class ValueSetEventHandler : IEventHandler<ValueSetEvent>
{
    private readonly ILogger<ValueSetEventHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ValueSetEventHandler(IServiceProvider serviceProvider, ILogger<ValueSetEventHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task HandleAsync(ValueSetEvent eventDto)
    {
        // Use the factory method to create a scoped IUnitOfWork instance
        using (var scope = _serviceProvider.CreateScope())
        {
            // Use unitOfWork as needed
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            _logger.LogInformation($"Value Changed From `{eventDto.OldValue}` To `{eventDto.NewValue}`");

            // Process Event1, update database, etc.
            await Task.Delay(2);
        }
    }
}