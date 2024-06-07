using Application.Contracts.Persistence;
using Application.Contracts.Presistence;

namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IBidRepository BidRepository { get; }
        IAssetRepository AssetRepository { get; }
        IAuctionRepository AuctionRepository { get; }
        ICollectionRepository CollectionRepository { get; }
        IProvenanceRepository ProvenanceRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        Task<int> SaveAsync();
    }
}
