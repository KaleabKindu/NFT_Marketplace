using Hangfire;
using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.Web3.Accounts;
using Microsoft.Extensions.Logging;
using Application.Contracts.Services;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class AuctionManagementService: IAuctionManagementService
{
    private readonly ILogger<AuctionManagementService> _logger;
    private readonly Contract _contract;

    public AuctionManagementService(
        ILogger<AuctionManagementService> logger,
        IConfiguration configuration
    ){
        _logger = logger;
        var account = new Account(configuration["SmartContract:PrivateKey"]);
        var web3 = new Web3(account, configuration["SmartContract:RpcUrl"]);
        _contract = web3.Eth.GetContract(configuration["SmartContract:Abi"], configuration["SmartContract:Address"]);
    }

    public async Task<bool> CloseAuction(string Address, long AuctionId)
    {
        try
        {
            var closeAuctionFunction = _contract.GetFunction("endAuction");
            var gas = await closeAuctionFunction.EstimateGasAsync(AuctionId);
            
            var transactionHash = await closeAuctionFunction.SendTransactionAsync(Address, new Nethereum.Hex.HexTypes.HexBigInteger(gas), null, AuctionId);

            _logger.LogInformation($"Transaction hash: {transactionHash}");

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while trying to close the auction.");
            return false;
        }
    }

    public string Schedule(string Address, long AuctionId, long AuctionEnd){
        _logger.LogInformation("********************** Scheduling close auction job...");
        long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return BackgroundJob.Schedule(() => CloseAuction(Address, AuctionId), TimeSpan.FromSeconds(AuctionEnd - unixTime + 5));
    }

    public void CancelAuction(long JobId){
        BackgroundJob.Delete(JobId);
    }
}
