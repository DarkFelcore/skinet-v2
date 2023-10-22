using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkinetV2.Domain.Products;
using SkinetV2.Domain.Products.ProductBrands.ValueObjects;
using SkinetV2.Domain.Products.ProductTypes.ValueObjects;

namespace SkinetV2.Application.common.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductBrandId brandId, ProductTypeId typeId)
        : base(x =>
                (!brandId.Value.HasValue || x.ProductBrandId == brandId) &&
                (!typeId.Value.HasValue || x.ProductTypeId == typeId)
            )
        {
        }
    }
}