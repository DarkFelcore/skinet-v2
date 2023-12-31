using ErrorOr;
using MediatR;
using SkinetV2.Domain.Baskets;
using SkinetV2.Domain.Baskets.BasketItems;

namespace SkinetV2.Application.Baskets.Update
{
    public record UpdateBasketCommand(
        string BasketId,
        List<BasketItem> BasketItems,
        Guid? DeliveryMethodId,
        string? ClientSecret,
        string? PaymentIntentId,
        decimal? ShippingPrice
    ) : IRequest<ErrorOr<CustomerBasket>>;
}