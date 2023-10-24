using SkinetV2.Domain.Baskets.BasketItems;

namespace SkinetV2.Domain.Baskets
{
    public class CustomerBasket
    {
        public string BasketId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();

        public CustomerBasket(string basketId, List<BasketItem> basketItems)
        {
            BasketId = basketId;
            BasketItems = basketItems;
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