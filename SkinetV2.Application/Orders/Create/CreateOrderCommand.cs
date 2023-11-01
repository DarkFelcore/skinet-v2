using ErrorOr;
using MediatR;
using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Orders.ValueObjects;

namespace SkinetV2.Application.Orders.Create
{
    public record CreateOrderCommand(
        string BasketId,
        string DeliveryMethodId,
        Address Address,
        string BuyerEmail
    ) : IRequest<ErrorOr<Order>>;
}