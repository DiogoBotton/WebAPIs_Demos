using Domains.Models.Users;
using Domains.SeedWork;

namespace Domains.Models.Users.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> EmailExists(string email, CancellationToken cancellationToken = default);
}
