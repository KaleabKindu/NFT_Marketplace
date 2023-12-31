using System.Text;
using Nethereum.Signer;
using Nethereum.Util;

namespace Application.Contracts.Services;

public interface IEthereumCryptoService
{
    public bool VerifyMessage(string message, string signature, string signerAddress);
}
