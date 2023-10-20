using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ProductBrands.ValueObjects;
using SkinetV2.Domain.Products.ProductTypes.ValueObjects;
using SkinetV2.Domain.Products.ValueObjects;

namespace SkinetV2.Infrastructure.Persistance.Specifications
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification(string sort, ProductBrandId brandId, ProductTypeId typeId, int skip, int take)
            : base(x => 
                (!brandId.Value.HasValue || x.ProductBrandId == brandId) &&
                (!typeId.Value.HasValue || x.ProductTypeId == typeId)
            )
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
            // Default order by product name, if no sorting is specified
            AddOrderBy(x => x.Name);

            ApplyPaging(skip, take);

            // If a sort option is passed through the url
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductsWithBrandsAndTypesSpecification(ProductId id)
            : base(x => x.ProductId == id)
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
        }
    }
}