using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Orders.ValueObjects;

namespace SkinetV2.Application.common.Specifications
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        // Get all orders
        public OrdersWithItemsAndOrderingSpecification(string email)
            : base(o => o.BuyerEmail == email)
        {
            AddIncludes(o => o.OrderItems);
            AddIncludes(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        // Get individual order
        public OrdersWithItemsAndOrderingSpecification(OrderId orderId, string email)
            : base(
                o => o.OrderId == orderId && o.BuyerEmail == email
            )
        {
            AddIncludes(o => o.OrderItems);
            AddIncludes(o => o.DeliveryMethod);
        }
    }
}