using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Events;

[Event("ValueSet")]
public class ValueSetEvent : IEventDTO
{
    [Parameter("uint", "oldValue", 1, true)]
    public uint OldValue { get; set; }

    [Parameter("uint", "newValue", 2, true)]
    public uint NewValue { get; set; }
}