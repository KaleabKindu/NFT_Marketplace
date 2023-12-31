namespace Application.Contracts.Persistance
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository UserRepository { get; }
        IBidRepository BidRepository {get; }
        IOfferRepository OfferRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<int> SaveAsync();
    }
}
