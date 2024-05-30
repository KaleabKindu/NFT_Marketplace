using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Nethereum.Util;

namespace Application.Contracts.Services;

public class EthereumCryptoService: IEthereumCryptoService
{
    private readonly Sha3Keccack _keccackHasher;

    public EthereumCryptoService(){
        _keccackHasher = new Sha3Keccack();
    }

    public bool VerifyMessage(string message, string signature, string signerAddress)
    {
        bool valid;
        try{
            var signatureECDS = EthECDSASignatureFactory.ExtractECDSASignature(signature);
            byte[] messageHash = _keccackHasher.CalculateHash(message.HexToByteArray());

            var ethEcKey = EthECKey.RecoverFromSignature(signatureECDS, messageHash);
            valid = ethEcKey.Verify(messageHash, signatureECDS);
        }catch{
            return false;
        }
        return valid;
    }
}
