using Application.Features.Assets.Dtos;
using Application.Features.Auth.Dtos;
using Application.Profiles;
using Domain.Transactions;

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
        public string Type { get; set; }
        public UserProfile Buyer { get; set; }
        public UserProfile Seller { get; set; }
        public AssetDetailDto Asset { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; }
        public string BlockchainTxHash { get; set; }
    }
}

