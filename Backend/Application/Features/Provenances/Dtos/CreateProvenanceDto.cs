using Domain;
using Domain.Assets;
using Domain.Provenances;

namespace Application.Features.Provenances.Dtos;

public class CreateProvenanceDto
{
    public Asset Asset { get; set; }
    public Event Event { get; set; }
    public AppUser From { get; set; }
    public AppUser To { get; set; }
    public string Price { get; set; }
    public string TransactionHash { get; set; }
}