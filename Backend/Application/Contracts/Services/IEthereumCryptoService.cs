namespace Application.Contracts.Services;

public interface IEthereumCryptoService
{
    public bool VerifyMessage(string message, string signature, string signerAddress);
}
