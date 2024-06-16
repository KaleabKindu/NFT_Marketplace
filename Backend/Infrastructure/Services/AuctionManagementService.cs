using Hangfire;
using Nethereum.Web3;
using System.Numerics;
using Nethereum.Contracts;
using Nethereum.Web3.Accounts;
using Microsoft.Extensions.Logging;
using Application.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Infrastructure.Services;

[Function("endAuction")]
public class EndAuctionFunction : FunctionMessage
{
    [Parameter("uint256", "_auctionId", 1)]
    public BigInteger AuctionId { get; set; }
}

public class AuctionManagementService: IAuctionManagementService
{
    private readonly ILogger<AuctionManagementService> _logger;
    private readonly Web3 _web3;

    public AuctionManagementService(
        ILogger<AuctionManagementService> logger,
        IConfiguration configuration
    ){
        _logger = logger;
        var account = new Account(configuration["SmartContract:PrivateKey"]);
        _web3 = new Web3(account, configuration["SmartContract:RpcUrl"]);
    }

    public async Task<bool> CloseAuction(string Address, long AuctionId)
    {
        _logger.LogInformation($"Closing Auction...");
        try
        {
            var transactionHandler = _web3.Eth.GetContractTransactionHandler<EndAuctionFunction>();
            var endAuctionFunction = new EndAuctionFunction()
            {
                AuctionId = AuctionId,
                FromAddress = Address,
                Gas = new BigInteger(300000), // Optionally set the gas limit
            };
            var result = await transactionHandler.SendRequestAndWaitForReceiptAsync(Address, endAuctionFunction);
            
            Console.WriteLine("Transaction successful: " + result.TransactionHash);

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
        return BackgroundJob.Schedule(() => CloseAuction(Address, AuctionId), TimeSpan.FromSeconds(AuctionEnd - unixTime));
    }

    public void CancelAuction(string JobId){
        BackgroundJob.Delete(JobId);
    }
}
