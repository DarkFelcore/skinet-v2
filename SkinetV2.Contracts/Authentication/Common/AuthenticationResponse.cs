using SkinetV2.Contracts.Authentication.Common;

namespace SkinetV2.Contracts.Authentication
{
    public record AuthenticationResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token,
        AddressResponse? Address
    );
}