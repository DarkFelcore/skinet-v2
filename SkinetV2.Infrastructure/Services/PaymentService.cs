using Microsoft.Extensions.Configuration;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Baskets;
using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Orders.ValueObjects;
using Stripe;

namespace SkinetV2.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            // Store secret key in the stripe api key
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            // Get customer basket
            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null) return null;

            // Shipping Price
            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.DeliveryMethodRepository.GetByIdAsync((Guid)basket.DeliveryMethodId);

                if (deliveryMethod is null)
                {
                    return null;
                }

                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.ProductRepository.GetByIdAsync(Guid.Parse(item.Id));

                if (productItem is null) return null;

                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            PaymentIntent intent;
            var service = new PaymentIntentService();

            // Check if we create or update a payment intent
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = basket.BasketItems.Sum(i => i.Quantity * ((long)i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "eur",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                // Create the intent
                intent = await service.CreateAsync(options);

                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<Order?> UpdateOrderPaymentFailed(string PaymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(PaymentIntentId);
            var order = await _unitOfWork.OrderRepository.GetEntityWithSpec(spec);

            if(order is null) return null;

            order.Status = OrderStatus.PaymentFailed;

            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }

        public async Task<Order?> UpdateOrderPaymentSucceeded(string PaymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(PaymentIntentId);
            var order = await _unitOfWork.OrderRepository.GetEntityWithSpec(spec);

            if(order is null) return null;

            order.Status = OrderStatus.PaymentReceived;

            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }
    }
}