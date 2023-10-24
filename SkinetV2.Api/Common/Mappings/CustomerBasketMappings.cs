using Mapster;
using SkinetV2.Application.Baskets.Delete;
using SkinetV2.Application.Baskets.Get;
using SkinetV2.Application.Baskets.Update;
using SkinetV2.Contracts.Baskets;
using SkinetV2.Domain.Baskets;

namespace SkinetV2.Api.Common.Mappings
{
    public class CustomerBasketMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetBasketByIdRequest, GetBasketByIdQuery>()
                .Map(dest => dest, src => src);

           
            config.NewConfig<CustomerBasket, UpdateBasketCommand>()
                .Map(dest => dest, src => src);

            config.NewConfig<DeleteBasketRequest, DeleteBasketCommand>()
                .Map(dest => dest, src => src);
        }
    }
}