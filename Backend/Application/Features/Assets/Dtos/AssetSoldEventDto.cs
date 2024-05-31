using System.Numerics;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Features.Assets.Dtos
{
    [Event("AssetSold")]
    public class AssetSoldEventDto : IEventDTO
    {
        [Parameter("uint256", "tokenId", 1, true)]
        public BigInteger TokenId { get; set; }

        [Parameter("address", "to", 2, false)]
        public string To { get; set; }
    }
}