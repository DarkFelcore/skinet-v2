using SkinetV2.Domain.Products.ValueObjects;

namespace SkinetV2.Domain.Products
{
    public class Product
    {
        public ProductId ProductId { get; set; }
        public string Name { get; set; }
    }
}