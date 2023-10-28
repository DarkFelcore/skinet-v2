using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;

namespace SkinetV2.Application.Authentication.Addresses.Get
{
    public record GetUserAddressQuery(string? Email) : IRequest<ErrorOr<AddressResult>>;
}