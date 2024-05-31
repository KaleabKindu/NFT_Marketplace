using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Contracts.Services;

public interface IEventHandler<TEvent> where TEvent: IEventDTO
{
    Task HandleAsync(TEvent eventData);
}