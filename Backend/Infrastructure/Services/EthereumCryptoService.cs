using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.Signer;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Application.Contracts.Services;

public class EthereumCryptoService: IEthereumCryptoService
{
    private readonly Web3 _web3;
    private readonly Sha3Keccack _keccackHasher;

    public EthereumCryptoService(){
        _keccackHasher = new Sha3Keccack();
        _web3 = new Web3();
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
