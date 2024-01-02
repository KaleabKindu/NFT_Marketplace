using Application.Contracts.Persistance;
using Application.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Events.Handlers;


public class ValueSetEventHandler : IEventHandler<ValueSetEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public ValueSetEventHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task HandleAsync(ValueSetEvent eventDto)
    {
        // Use the factory method to create a scoped IUnitOfWork instance
        using (var scope = _serviceProvider.CreateScope())
        {
            // Use unitOfWork as needed
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            Console.WriteLine($"Value Changed From `{eventDto.OldValue}` To `{eventDto.NewValue}`");
            // Process Event1, update database, etc.
            await Task.Delay(2);
        }
    }
}