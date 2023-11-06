using Mapster;
using SkinetV2.Application.Payments.CreateUpdatePaymentIntent;
using SkinetV2.Contracts.Payments;

namespace SkinetV2.Api.Common.Mappings
{
    public class PaymentMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateOrUpdatePaymentIntentRequest, CreateOrUpdatePaymentIntentCommand>()
                .Map(dest => dest, src => src);
        }
    }
}