using Application.Contracts.Persistance;
using Domain.Transactions;
using Domain;

namespace Application.Contracts.Persistence
{
    public interface ITransactionRepository:IRepository<Transaction>{
        Task<IEnumerable<Transaction>> GetAllTransactionAsync(int page=1, int limit=10, int assetId=0);

        Task<IDictionary<AppUser, double>> GetCreatorSalesVolumeAsync(int page=1, int limit=10);

     }

}