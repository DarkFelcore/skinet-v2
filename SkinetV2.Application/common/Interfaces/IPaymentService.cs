using SkinetV2.Domain.Baskets;
using SkinetV2.Domain.Orders;

namespace SkinetV2.Application.common.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order?> UpdateOrderPaymentSucceeded(string PaymentIntentId);
        Task<Order?> UpdateOrderPaymentFailed(string PaymentIntentId);
    }
}