using System.Numerics;
using Application.Common;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Features.Auctions.Dtos
{
    [Event("AuctionCanceled")]
    public class AuctionCancelledEventDto : EventDto
    {
        [Parameter("uint256", "auctionId", 1, true)]
        public BigInteger AuctionId { get; set; }

        [Parameter("address", "highestBidder", 2, false)]
        public string HighestBidder { get; set; }

        [Parameter("uint256", "highestBid", 3, false)]
        public BigInteger HighestBid { get; set; }
    }
}