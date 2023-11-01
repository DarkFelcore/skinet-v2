using ErrorOr;
using MediatR;
using SkinetV2.Domain.Orders.Entities;

namespace SkinetV2.Application.DeliveryMethods.All
{
    public record GetAllDeliveryMethodsQuery() : IRequest<ErrorOr<List<DeliveryMethod>>>;
}