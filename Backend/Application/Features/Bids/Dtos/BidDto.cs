
using Application.Features.Common;

namespace Application.Features.Bids.Dtos
{
    public class BidDto: BaseDto
    {
        public string Bidder { set; get; }

        public long AssetId { set; get; }

        public double Amount { set; get; }

        public string TransactionHash { set; get; }
    }
}