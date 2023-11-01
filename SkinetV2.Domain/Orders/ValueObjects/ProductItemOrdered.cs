namespace SkinetV2.Domain.Orders.ValueObjects
{
    public class ProductItemOrdered
    {
        public Guid ProductItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;

        public ProductItemOrdered(Guid productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public ProductItemOrdered()
        {

        }
    }
}