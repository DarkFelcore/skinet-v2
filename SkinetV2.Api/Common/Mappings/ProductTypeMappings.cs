using Mapster;
using SkinetV2.Contracts.ProductTypes;
using SkinetV2.Domain.Products.ProductTypes;

namespace SkinetV2.Api.Common.Mappings
{
    public class ProductTypeMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductType, ProductTypesReponse>()
                .Map(dest => dest.ProductTypeId, src => src.ProductTypeId.Value);
        }
    }
}