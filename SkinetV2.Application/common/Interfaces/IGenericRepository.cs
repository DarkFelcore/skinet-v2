namespace SkinetV2.Application.common.Interfaces
{
    public interface IGenericRepository<T>
        where T: class
    {
        Task<List<T>> AllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
    }
}