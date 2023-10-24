using ErrorOr;
using MediatR;
using SkinetV2.Domain.Baskets;

namespace SkinetV2.Application.Baskets.Get
{
    public record GetBasketByIdQuery(string BasketId) : IRequest<ErrorOr<CustomerBasket>>;
}