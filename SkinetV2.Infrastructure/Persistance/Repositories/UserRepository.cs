using Microsoft.EntityFrameworkCore;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Users;

namespace SkinetV2.Infrastructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StoreContext context) 
            : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}