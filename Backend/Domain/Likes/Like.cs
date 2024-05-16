namespace Domain.Assets
{
    public class Like : BaseClass
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public Asset Asset { get; set; }
        public long AssetId { get; set; }
    }
}