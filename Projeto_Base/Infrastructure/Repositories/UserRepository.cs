using Domains.Models.Users;
using Domains.Models.Users.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApiDbContext context) : base(context)
    {
    }

    public async Task<bool> EmailExists(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AnyAsync(x => x.Email == email, cancellationToken);
    }
}
