using Mapster;
using SkinetV2.Application.Orders.ById;
using SkinetV2.Application.Orders.Create;
using SkinetV2.Contracts.Orders;
using SkinetV2.Domain.Orders;
using SkinetV2.Domain.Orders.Entities;
using SkinetV2.Domain.Orders.ValueObjects;

namespace SkinetV2.Api.Common.Mappings
{
    public class OrderMappings : IRegister
    {
        const string apiUrl = "https://localhost:7075/";
        
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(string BuyerEmail, CreateOrderRequest Request), CreateOrderCommand>()
                .Map(dest => dest.BuyerEmail, src => src.BuyerEmail)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<ProductItemOrdered, ProductItemOrderedReponse>()
                .Map(dest => dest.PictureUrl, src => src.PictureUrl != null ? apiUrl + src.PictureUrl : src.PictureUrl)
                .Map(dest => dest, src => src);

            config.NewConfig<OrderItem, OrderItemResponse>()
                .Map(dest => dest.OrderItemId, src => src.OrderItemId.Value)
                .Map(dest => dest, src => src);

            config.NewConfig<Address, AddressResponse>()
                .Map(dest => dest, src => src);

            config.NewConfig<DeliveryMethod, DeliveryMethodResponse>()
                .Map(dest => dest.DeliveryMethodId, src => src.DeliveryMethodId.Value)
                .Map(dest => dest, src => src);

            config.NewConfig<Order, OrderReponse>()
                .Map(dest => dest.OrderId, src => src.OrderId.Value)
                .Map(dest => dest, src => src);
        }
    }
}