using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Events;

[Event("ValueSet")]
public class ValueSetEvent : IEventDTO
{
    [Parameter("uint256", "oldValue", 1, true)]
    public uint OldValue { get; set; }

    [Parameter("uint256", "newValue", 2, true)]
    public uint NewValue { get; set; }
}