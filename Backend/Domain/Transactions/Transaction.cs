using Domain.Assets;

namespace Domain.Transactions
{
    public enum TransactionStatus
    {
        Pending,
        Completed,
        Canceled
    }
    public class Transaction : BaseClass
    {
        public AppUser Buyer { get; set; }
        public AppUser Seller { get; set; }
        public Asset Asset { get; set; }
        public double Amount { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public string BlockchainTxHash { get; set; }
    }
}

