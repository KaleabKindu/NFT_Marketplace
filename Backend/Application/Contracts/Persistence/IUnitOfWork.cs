using Application.Contracts.Persistence;
using Application.Contracts.Presistence;

namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository UserRepository { get; }
        IBidRepository BidRepository { get; }
        IAssetRepository AssetRepository{ get; }
        ITransactionRepository TransactionRepository{ get;}
        IAuctionRepository AuctionRepository{ get; }
        ICollectionRepository CollectionRepository { get; }
        IProvenanceRepository ProvenanceRepository { get; }

        Task<int> SaveAsync();
    }
}
