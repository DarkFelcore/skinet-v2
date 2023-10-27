using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ProductBrands.ValueObjects;
using SkinetV2.Domain.Products.ProductTypes.ValueObjects;

namespace SkinetV2.Application.common.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductBrandId brandId, ProductTypeId typeId, string search)
        : base(x =>
                (string.IsNullOrEmpty(search) || x.Name.ToLower().Contains(search)) &&
                (!brandId.Value.HasValue || x.ProductBrandId == brandId) &&
                (!typeId.Value.HasValue || x.ProductTypeId == typeId)
            )
        {
        }
    }
}