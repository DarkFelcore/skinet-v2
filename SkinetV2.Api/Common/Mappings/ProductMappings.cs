using Mapster;
using SkinetV2.Contracts.Products;
using SkinetV2.Domain.Products;

namespace SkinetV2.Api.Common.Mappings
{
    public class ProductMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductResponse>()
                .Map(dest => dest.ProductId, src => src.ProductId.Value)
                .Map(dest => dest, src => src);
        }
    }
}