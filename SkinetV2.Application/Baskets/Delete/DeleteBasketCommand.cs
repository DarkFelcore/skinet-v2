using ErrorOr;
using MediatR;

namespace SkinetV2.Application.Baskets.Delete
{
    public record DeleteBasketCommand(string BasketId) : IRequest<ErrorOr<bool>>;
}