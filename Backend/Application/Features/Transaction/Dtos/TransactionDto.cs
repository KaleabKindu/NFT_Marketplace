namespace Application.Features.Trasactions.Dtos
{
    public enum TransactionStatus
    {
        Pending,
        Completed,
        Canceled
    }
    public class TransactionDto
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int AssetId { get; set; }
        public float Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public string BlockchainTxHash { get; set; }
        public string TokenId { get; set; }

    }
}

