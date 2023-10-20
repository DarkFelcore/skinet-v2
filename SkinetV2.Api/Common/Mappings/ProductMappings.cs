using Mapster;
using SkinetV2.Application.Helpers;
using SkinetV2.Application.Products.Get.All;
using SkinetV2.Contracts.Products;
using SkinetV2.Domain.Products;

namespace SkinetV2.Api.Common.Mappings
{
    public class ProductMappings : IRegister
    {
        const string apiUrl = "https://localhost:7075/";
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductSearchParams, GetAllProductsQuery>()
                .Map(dest => dest.Sort, src => src.Sort)
                .Map(dest => dest.BrandId, src => src.BrandId)
                .Map(dest => dest.TypeId, src => src.TypeId);

            config.NewConfig<Product, ProductResponse>()
                .Map(dest => dest.ProductId, src => src.ProductId.Value)
                .Map(dest => dest.ProductBrandName, src => src.ProductBrand.Name)
                .Map(dest => dest.ProductTypeName, src => src.ProductType.Name)
                .Map(dest => dest.PictureUrl, src => src.PictureUrl != null ? apiUrl + src.PictureUrl : src.PictureUrl)
                .Map(dest => dest, src => src);
        }
    }
}