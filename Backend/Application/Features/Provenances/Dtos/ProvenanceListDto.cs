using Application.Features.Auth.Dtos;

namespace Application.Features.Provenances.Dtos;

public class ProvenanceListDto
{
    public string Event { get; set; }
    public UserFetchDto From { get; set; }
    public UserFetchDto To { get; set; }
    public string Price { get; set; }
    public string TransactionHash { get; set; }
    public DateTime Date { get; set; }
}