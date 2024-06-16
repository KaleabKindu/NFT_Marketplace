namespace Application.Contracts.Services;

public interface IAuctionManagementService
{
    public Task<bool> CloseAuction(long AuctionId);
    public string Schedule(long AuctionId, long AuctionEnd);
    public void CancelAuction(string JobId);
}
