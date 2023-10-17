using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }

        public override async Task<Product?> GetByIdAsync(Guid id)
        {
            var productId = new ProductId(id);
            return await _context.Products.FindAsync(productId);
        }
    }
}