using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Application.common.Specifications;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Products;

namespace SkinetV2.Application.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IPaymentService _paymentService;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _paymentService = paymentService;
        }

        public async Task<ErrorOr<Order>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(command.BasketId);
            var deliveryMethod = await _unitOfWork.DeliveryMethodRepository.GetByIdAsync(Guid.Parse(command.DeliveryMethodId));


            if (basket is null)
            {
                return Errors.Basket.NotFound;
            }

            if (deliveryMethod is null)
            {
                return Errors.DeliveryMedthod.NotFound;
            }

            var productsInBasket = new List<Product>();
            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(Guid.Parse(item.Id));
                if (product is not null)
                {
                    productsInBasket.Add(product);
                }
            }

            //Check to see if order already exists based on payment intent id
            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId!);
            var existingOrder = await _unitOfWork.OrderRepository.GetEntityWithSpec(spec);

            if (existingOrder is not null)
            {
                await _unitOfWork.OrderRepository.DeleteAsync(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.BasketId);
            }


            var order = await _unitOfWork.OrderRepository.CreateOrderAsync(command.BuyerEmail, deliveryMethod, basket, productsInBasket, command.Address);

            if (order is null)
            {
                return Errors.Order.ProblemCreatingOrder;
            }

            return order;
        }
    }
}