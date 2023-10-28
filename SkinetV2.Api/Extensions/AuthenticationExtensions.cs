using System.Security.Claims;

namespace SkinetV2.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static string GetEmailByClaimTypesAsync(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
        }
    }
}