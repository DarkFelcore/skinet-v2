using SkinetV2.Domain.Products.ProductBrands;
using SkinetV2.Domain.Products.ProductBrands.ValueObjects;
using SkinetV2.Domain.Products.ProductTypes;
using SkinetV2.Domain.Products.ProductTypes.ValueObjects;
using SkinetV2.Domain.Products.ValueObjects;

namespace SkinetV2.Domain.Products
{
    public class Product
    {
        public ProductId ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public ProductTypeId ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public ProductBrandId ProductBrandId { get; set; }
    }
}