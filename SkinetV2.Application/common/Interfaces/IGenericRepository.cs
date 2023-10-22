namespace SkinetV2.Application.common.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<List<T>> AllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<List<T>> GetListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}