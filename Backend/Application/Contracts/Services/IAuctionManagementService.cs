namespace Application.Contracts.Services;

public interface IAuctionManagementService
{
    public Task<bool> CloseAuction(string Address, long AuctionId);
    public void Schedule(string Address, long AuctionId, long AuctionEnd);
}
