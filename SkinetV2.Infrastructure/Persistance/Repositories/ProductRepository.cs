using Microsoft.EntityFrameworkCore;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ProductBrands;
using SkinetV2.Domain.Products.ProductTypes;
using SkinetV2.Domain.Products.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }

        public override async Task<List<Product>> AllAsync()
        {
            return await _context.Products
                .Include(x => x.ProductBrand)
                .Include(x => x.ProductType)
                .ToListAsync();
        }

        public async Task<List<ProductBrand>> GetAllProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<List<ProductType>> GetAllProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public override async Task<Product?> GetByIdAsync(Guid id)
        {
            var productId = new ProductId(id);
            return await _context.Products
                .Include(x => x.ProductBrand)
                .Include(x => x.ProductType)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }
    }
}