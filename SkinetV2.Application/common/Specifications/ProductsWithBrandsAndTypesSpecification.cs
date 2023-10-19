using System.Linq.Expressions;
using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance.Specifications
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification()
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
        }

        public ProductsWithBrandsAndTypesSpecification(ProductId id)
            : base(x => x.ProductId == id)
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
        }
    }
}