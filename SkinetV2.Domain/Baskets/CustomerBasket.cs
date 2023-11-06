using SkinetV2.Domain.Baskets.BasketItems;

namespace SkinetV2.Domain.Baskets
{
    public class CustomerBasket
    {
        public string BasketId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();
        public Guid? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public decimal? ShippingPrice { get; set; }

        public CustomerBasket(string basketId, List<BasketItem> basketItems, Guid? deliveryMethodId, string? clientSecret, string? paymentIntentId, decimal? shippingPrice)
        {
            BasketId = basketId;
            BasketItems = basketItems;
            DeliveryMethodId = deliveryMethodId;
            ClientSecret = clientSecret;
            PaymentIntentId = paymentIntentId;
            ShippingPrice = shippingPrice;
        }
        public CustomerBasket(string basketId)
        {
            BasketId = basketId;
        }

        public CustomerBasket()
        {

        }
    }
}