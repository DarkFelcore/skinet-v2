using ErrorOr;
using MediatR;
using SkinetV2.Application.Baskets.Update;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Baskets;
using SkinetV2.Domain.Common.Errors;

namespace SkinetV2.Application.Baskets.Create
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, ErrorOr<CustomerBasket>>
    {
        private readonly IBasketRepository _basketRepository;

        public UpdateBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ErrorOr<CustomerBasket>> Handle(UpdateBasketCommand command, CancellationToken cancellationToken)
        {
            var customerBasket = new CustomerBasket(command.BasketId, command.BasketItems);
            var basket = await _basketRepository.UpdateBasketAsync(customerBasket);

            if(basket is null)
            {
                return Errors.Basket.NotFound;
            }

            return basket;
        }
    }
}