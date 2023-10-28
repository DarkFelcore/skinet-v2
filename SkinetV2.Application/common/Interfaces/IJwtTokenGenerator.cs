using SkinetV2.Domain.Users;

namespace SkinetV2.Application.common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}