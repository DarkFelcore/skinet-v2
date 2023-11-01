using SkinetV2.Domain.Baskets;
using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Orders.Entities;
using SkinetV2.Domain.Orders.ValueObjects;
using SkinetV2.Domain.Products;

namespace SkinetV2.Application.common.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> CreateOrderAsync(string buyerEmail, DeliveryMethod deliveryMethod, CustomerBasket basket, List<Product> productsInBasket, Address shippingAddress);
    }
}