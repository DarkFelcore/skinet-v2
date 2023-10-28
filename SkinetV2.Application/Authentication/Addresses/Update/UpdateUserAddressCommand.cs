using ErrorOr;
using MediatR;
using SkinetV2.Application.Authentication.Common;

namespace SkinetV2.Application.Authentication.Addresses.Update
{
    public record UpdateUserAddressCommand(
        string Email,
        string Street,
        string PostalCode,
        string Provice,
        string City,
        string Country
    ) : IRequest<ErrorOr<AddressResult>>;
}