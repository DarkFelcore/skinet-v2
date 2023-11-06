using ErrorOr;
using MediatR;
using SkinetV2.Domain.Baskets;

namespace SkinetV2.Application.Payments.CreateUpdatePaymentIntent
{
    public record CreateOrUpdatePaymentIntentCommand(
        string BasketId
    ) : IRequest<ErrorOr<CustomerBasket?>>;
}