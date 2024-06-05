using System.Numerics;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Application.Common;

namespace Application.Features.Assets.Dtos
{
    [Event("TransferAsset")]
    public class TransferAssetEventDto : EventDto
    {
        [Parameter("uint256", "tokenId", 1, true)]
        public BigInteger TokenId { get; set; }

        [Parameter("address", "newOwner", 2, false)]
        public string NewOwner { get; set; }
    }
}