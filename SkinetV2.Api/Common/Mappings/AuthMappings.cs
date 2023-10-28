using Mapster;
using SkinetV2.Application.Authentication.Addresses.Update;
using SkinetV2.Application.Authentication.Common;
using SkinetV2.Application.Authentication.Login;
using SkinetV2.Application.Authentication.Register;
using SkinetV2.Contracts.Authentication;
using SkinetV2.Contracts.Authentication.Common;

namespace SkinetV2.Api.Common.Mappings
{
    public class AuthMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>()
                .Map(dest => dest, src => src);

            config.NewConfig<LoginRequest, LoginQuery>()
                .Map(dest => dest, src => src);

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Id, src => src.User.UserId.Value)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.Address, src => src.User.Address.Adapt<AddressResponse>())
                .Map(dest => dest, src => src);
            
            config.NewConfig<AddressResult, AddressResponse>()
                .Map(dest => dest, src => src);

            config.NewConfig<(string Email, UpdateUserAddressRequest Request), UpdateUserAddressCommand>()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest, src => src.Request);
        }
    }
}