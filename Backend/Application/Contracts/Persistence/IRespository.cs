namespace Application.Contracts.Persistance
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<bool> Exists(long d);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }

}