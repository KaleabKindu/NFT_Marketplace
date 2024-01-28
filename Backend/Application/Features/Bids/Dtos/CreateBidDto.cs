
namespace Application.Features.Bids.Dtos
{
    public class CreateBidDto
    {
        public long AssetId { set; get; }

        public double Amount { set; get; }

        public string TransactionHash { set; get; }
    }
}