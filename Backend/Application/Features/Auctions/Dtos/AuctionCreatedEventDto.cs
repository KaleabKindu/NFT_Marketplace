using System.Numerics;
using Application.Common;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Features.Auctions.Dtos
{
    [Event("AuctionCreated")]
    public class AuctionCreatedEventDto : EventDto
    {
        [Parameter("uint256", "auctionId", 1, true)]
        public BigInteger AuctionId { get; set; }

        [Parameter("uint256", "tokenId", 2, false)]
        public BigInteger TokenId { get; set; }

        [Parameter("address", "seller", 3, false)]
        public string Seller { get; set; }

        [Parameter("uint256", "floorPrice", 4, false)]
        public BigInteger FloorPrice { get; set; }

        [Parameter("uint256", "auctionEnd", 5, false)]
        public BigInteger AuctionEnd { get; set; }
    }
}