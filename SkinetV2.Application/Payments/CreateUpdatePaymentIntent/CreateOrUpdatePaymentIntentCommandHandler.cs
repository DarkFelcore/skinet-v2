using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Baskets;

namespace SkinetV2.Application.Payments.CreateUpdatePaymentIntent
{
    public class CreateOrUpdatePaymentIntentCommandHandler : IRequestHandler<CreateOrUpdatePaymentIntentCommand, ErrorOr<CustomerBasket?>>
    {
        private readonly IPaymentService _paymentService;

        public CreateOrUpdatePaymentIntentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<ErrorOr<CustomerBasket?>> Handle(CreateOrUpdatePaymentIntentCommand command, CancellationToken cancellationToken)
        {
            return await _paymentService.CreateOrUpdatePaymentIntent(command.BasketId);
        }
    }
}