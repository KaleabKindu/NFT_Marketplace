using System.Numerics;
using Application.Common;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Features.Auctions.Dtos
{
    [Event("AuctionEnded")]
    public class AuctionEndedEventDto : EventDto
    {
        [Parameter("uint256", "auctionId", 1, true)]
        public BigInteger AuctionId { get; set; }

        [Parameter("address", "winner", 2, false)]
        public string Winner { get; set; }
    }
}