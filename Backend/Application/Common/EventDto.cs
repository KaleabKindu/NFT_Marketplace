using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Common
{
    public class EventDto : IEventDTO
    {
        public string TransactionHash { get; set; }
    }
}