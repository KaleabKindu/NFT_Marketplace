#nullable enable
using Domain.Assets;

namespace Domain.Provenances;


public enum Event
{
    Sale,
    Transfer,
    Mint
}

public class Provenance : BaseClass
{
    public Asset Asset { get; set; }
    public long AssetId { get; set; }
    public Event Event { get; set; }
    public AppUser From { get; set; }
    public string FromId { get; set; }
    public AppUser? To { get; set; }
    public string? ToId { get; set; }
    public double Price { get; set; }
    public string TransactionHash { get; set; }
}