using ErrorOr;
using MediatR;
using SkinetV2.Domain.Orders;

namespace SkinetV2.Application.Orders.ById
{
    public record GetOrderByIdQuery(
        Guid OrderId,
        string BuyerEmail
    ) : IRequest<ErrorOr<Order>>;
}