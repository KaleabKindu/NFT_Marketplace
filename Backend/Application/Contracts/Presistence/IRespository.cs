namespace Application.Contracts.Presistence
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int PageSize);
        Task<T> AddAsync(T entity);
        Task<bool> Exists(int d);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }

}