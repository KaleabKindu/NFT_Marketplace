using System.Numerics;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Features.Assets.Dtos
{
    [Event("ResellAsset")]
    public class ResellAssetEventDto : IEventDTO
    {
        [Parameter("uint256", "tokenId", 1, true)]
        public BigInteger TokenId { get; set; }

        [Parameter("bool", "auction", 2, false)]
        public bool Auction { get; set; }

        [Parameter("uint256", "price", 3, false)]
        public BigInteger Price { get; set; }

        [Parameter("uint256", "auctionId", 4, false)]
        public BigInteger AuctionId { get; set; }

        [Parameter("uint256", "auctionEnd", 5, false)]
        public BigInteger AuctionEnd { get; set; }
    }
}