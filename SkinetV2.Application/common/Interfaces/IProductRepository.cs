using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ProductBrands;
using SkinetV2.Domain.Products.ProductTypes;

namespace SkinetV2.Application.common.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<ProductBrand>> GetAllProductBrandsAsync();
        Task<List<ProductType>> GetAllProductTypesAsync();
    }
}