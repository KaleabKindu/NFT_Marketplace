#nullable enable
using Domain.Assets;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
    public Event Event { get; set; }
    public AppUser From { get; set; }
    public AppUser? To { get; set; }
    public double Price { get; set; }
    public string TransactionHash { get; set; }
    

}