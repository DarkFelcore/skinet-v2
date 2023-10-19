using Mapster;
using SkinetV2.Contracts.ProductBrands;
using SkinetV2.Domain.Products.ProductBrands;

namespace SkinetV2.Api.Common.Mappings
{
    public class ProductBrandsMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductBrand, ProductBrandsReponse>()
                .Map(dest => dest.ProductBrandId, src => src.ProductBrandId.Value);
        }
    }
}