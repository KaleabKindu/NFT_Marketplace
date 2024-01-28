namespace Domain.Trasactions
{
    public enum TransactionStatus
    {
        Pending,
        Completed,
        Canceled
    }
    public class Transaction : BaseClass
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int AssetId { get; set; }
        public float Amount { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public string BlockchainTxHash { get; set; }
        public string TokenId { get; set; }

    }
}

