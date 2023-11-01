using ErrorOr;
using MediatR;
using SkinetV2.Domain.Orders;

namespace SkinetV2.Application.Orders.AllForUser
{
    public record GetOrdersForUserQuery(
        string BuyerEmail
    ) : IRequest<ErrorOr<List<Order>>>;
}