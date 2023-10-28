using SkinetV2.Domain.Users;

namespace SkinetV2.Application.common.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}