using SkinetV2.Domain.Orders.Entities.ValueObjects;
using SkinetV2.Domain.Orders.ValueObjects;

namespace SkinetV2.Domain.Orders.Entities
{
    public class OrderItem
    {
        public OrderItemId OrderItemId { get; set; } = null!;
        public ProductItemOrdered ItemOrdered { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            OrderItemId = new OrderItemId(Guid.NewGuid());
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public OrderItem()
        {
        }
    }
}