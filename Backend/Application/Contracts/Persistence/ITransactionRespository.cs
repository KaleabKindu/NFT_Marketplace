using Application.Contracts.Persistance;
using Domain.Trasactions;

namespace Application.Contracts.Persistence
{
    public interface ITransactionRepository:IRepository<Transaction>{
        Task<IEnumerable<Transaction>> GetAllTransactionAsync(int page=1, int limit=10, int assetId=0);

     }

}