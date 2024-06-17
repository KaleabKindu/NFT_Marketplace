using System;
using System.Threading.Tasks;
using Hangfire;
using Nethereum.Contracts;
using Nethereum.Web3.Accounts;
using Microsoft.Extensions.Logging;
using Application.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Nethereum.Web3;

namespace Application.UnitTest.Mocks
{
    public class MockAuctionManagementService : IAuctionManagementService
    {
        private readonly ILogger<MockAuctionManagementService> _logger;
        private readonly Contract _contract;

        public MockAuctionManagementService(
            ILogger<MockAuctionManagementService> logger,
            IConfiguration configuration
        )
        {
            _logger = logger;
            var account = new Account(configuration["SmartContract:PrivateKey"]);
            var web3 = new Web3(account, configuration["SmartContract:RpcUrl"]);
            _contract = web3.Eth.GetContract(configuration["SmartContract:Abi"], configuration["SmartContract:Address"]);
        }

        public async Task<bool> CloseAuction(string Address, long AuctionId)
        {
            try
            {
                // Mock the behavior of closing an auction
                _logger.LogInformation($"Closing auction with ID: {AuctionId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to close the auction.");
                return false;
            }
        }

        public string Schedule(string Address, long AuctionId, long AuctionEnd)
        {
            // Mock the behavior of scheduling a close auction job
            _logger.LogInformation("********************** Scheduling close auction job...");
            long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var jobId = BackgroundJob.Schedule(() => CloseAuction(Address, AuctionId), TimeSpan.FromSeconds(AuctionEnd - unixTime + 5));
            _logger.LogInformation($"Scheduled close auction job with ID: {jobId}");
            return jobId;
        }

        public void CancelAuction(string JobId)
        {
            // Mock the behavior of cancelling a scheduled close auction job
            _logger.LogInformation($"Cancelling scheduled close auction job with ID: {JobId}");
            BackgroundJob.Delete(JobId);
        }
    }
}