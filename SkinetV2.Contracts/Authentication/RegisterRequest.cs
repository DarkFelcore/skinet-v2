using SkinetV2.Contracts.Authentication.Common;

namespace SkinetV2.Contracts.Authentication
{
    public record RegisterRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        AddressRequest Address
    );
}