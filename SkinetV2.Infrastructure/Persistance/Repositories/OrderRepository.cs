using SkinetV2.Application.common.Interfaces;
using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Baskets;
using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Orders.Entities;
using SkinetV2.Domain.Orders.ValueObjects;
using SkinetV2.Domain.Products;

namespace SkinetV2.Infrastructure.Persistance.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(StoreContext context) : base(context)
        {
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, DeliveryMethod deliveryMethod, CustomerBasket basket, List<Product> productsInBasket, Address shippingAddress)
        {
            // Get order items
            var orderItems = new List<OrderItem>();
            for (int i = 0; i < basket.BasketItems.Count; i++)
            {
                var product = productsInBasket[i];
                var basketItem = basket.BasketItems[i];

                var itemOrdered = new ProductItemOrdered(product.ProductId.Value, product.Name, product.PictureUrl, product.ProductType.Name);
                var orderItem = new OrderItem(itemOrdered, product.Price, basketItem.Quantity);

                orderItems.Add(orderItem);
            }

            // Calc subtotal
            var subtotal = orderItems.Sum(x => x.Price * x.Quantity);

            // Create order
            var order = new Order(orderItems, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId!);

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}