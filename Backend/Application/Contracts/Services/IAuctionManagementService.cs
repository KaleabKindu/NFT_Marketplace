namespace Application.Contracts.Services;

public interface IAuctionManagementService
{
    public Task<bool> CloseAuction(string Address, long AuctionId);
    public string Schedule(string Address, long AuctionId, long AuctionEnd);
    public void CancelAuction(string JobId);
}
