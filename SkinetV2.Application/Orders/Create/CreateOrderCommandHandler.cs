using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Products;

namespace SkinetV2.Application.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
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

            var order = await _unitOfWork.OrderRepository.CreateOrderAsync(command.BuyerEmail, deliveryMethod, basket, productsInBasket, command.Address);

            if (order is null)
            {
                return Errors.Order.ProblemCreatingOrder;
            }

            return order;
        }
    }
}