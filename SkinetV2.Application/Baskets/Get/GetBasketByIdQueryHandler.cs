using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Baskets;

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
            return basket ?? new CustomerBasket(query.BasketId);
        }
    }
}