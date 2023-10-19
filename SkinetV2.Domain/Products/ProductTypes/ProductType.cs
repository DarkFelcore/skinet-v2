using SkinetV2.Domain.Products.ProductTypes.ValueObjects;

namespace SkinetV2.Domain.Products.ProductTypes
{
    public class ProductType
    {
        public ProductTypeId ProductTypeId { get; set; }
        public string Name { get; set; }
    }
}