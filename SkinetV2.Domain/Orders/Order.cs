using SkinetV2.Domain.Orders.Entities;
using SkinetV2.Domain.Orders.ValueObjects;

namespace SkinetV2.Domain.Orders
{
    public class Order
    {
        public OrderId OrderId { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = new();
        public string BuyerEmail { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = new();
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }

        public Order(List<OrderItem> orderItems, string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, decimal subtotal)
        {
            OrderId = new OrderId(Guid.NewGuid());
            OrderItems = orderItems;
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
        }

        public Order()
        {
            
        }
    }
}