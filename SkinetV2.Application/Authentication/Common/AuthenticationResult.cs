using SkinetV2.Domain.Users;

namespace SkinetV2.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token
    );
}