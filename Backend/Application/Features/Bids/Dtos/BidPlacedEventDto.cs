using System.Numerics;
using Application.Common;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Features.Bids.Dtos
{
    [Event("BidPlaced")]
    public class BidPlacedEventDto : EventDto
    {
        [Parameter("uint256", "auctionId", 1, true)]
        public BigInteger AuctionId { get; set; }

        [Parameter("address", "bidder", 2, false)]
        public string Bidder { get; set; }

        [Parameter("uint256", "amount", 3, false)]
        public BigInteger Amount { get; set; }
    }
}