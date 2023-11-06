namespace SkinetV2.Domain.Orders.ValueObjects
{
    public class ProductItemOrdered
    {
        public Guid ProductItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public string ProductType { get; set; }

        public ProductItemOrdered(Guid productItemId, string productName, string pictureUrl, string productType)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            ProductType = productType;
        }

        public ProductItemOrdered()
        {

        }
    }
}