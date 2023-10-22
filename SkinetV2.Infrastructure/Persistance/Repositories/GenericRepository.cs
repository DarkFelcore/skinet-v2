using Microsoft.EntityFrameworkCore;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Infrastructure.Persistance.Specifications;

namespace SkinetV2.Infrastructure.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly StoreContext _context;
        internal DbSet<T> DbSet;
        public GenericRepository(StoreContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async Task<bool> AddAsync(T item)
        {
            await DbSet.AddAsync(item);
            return true;
        }

        public virtual async Task<List<T>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<bool> DeleteAsync(T item)
        {
            DbSet.Remove(item);
            return true;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T item)
        {
            DbSet.Update(item);
            return true;
        }

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        // This method is responsible for adding the where, includes, ect. from the specifications passed as parameter
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(DbSet.AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
    }
}