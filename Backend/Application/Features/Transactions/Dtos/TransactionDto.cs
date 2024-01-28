using Application.Features.Assets.Dtos;
using Application.Features.Auth.Dtos;

namespace Application.Features.Transactions.Dtos
{
    public enum TransactionStatus
    {
        Pending,
        Completed,
        Canceled
    }
    public class TransactionDto
    {
        public long Id { get; set; }
        public UserDto Buyer { get; set; }
        public UserDto Seller { get; set; }
        public AssetDto Asset { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; }
        public string BlockchainTxHash { get; set; }
    }
}

