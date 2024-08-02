using Domains.Models.Users;
using Domains.Models.Users.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApiDbContext context) : base(context)
    {
    }
}
