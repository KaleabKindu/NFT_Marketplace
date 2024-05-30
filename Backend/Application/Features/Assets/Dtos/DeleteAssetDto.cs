using System.Numerics;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Application.Features.Assets.Dtos
{
    [Event("DeleteAsset")]
    public class DeleteAssetEventDto : IEventDTO
    {
        [Parameter("uint256", "tokenId", 1, false)]
        public BigInteger TokenId { get; set; }
    }
}