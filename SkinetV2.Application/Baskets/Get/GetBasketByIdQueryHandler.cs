using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Baskets;
using SkinetV2.Domain.Common.Errors;

namespace SkinetV2.Application.Baskets.Get
{
    public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQuery, ErrorOr<CustomerBasket>>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByIdQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ErrorOr<CustomerBasket>> Handle(GetBasketByIdQuery query, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(query.BasketId);
            
            if(basket is null)
            {
                return Errors.Basket.EmptyBasket;
            }

            return basket;
        }
    }
}