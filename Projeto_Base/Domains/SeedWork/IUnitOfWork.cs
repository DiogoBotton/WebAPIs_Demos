namespace Domains.SeedWork;

public interface IUnitOfWork
{
    Task SaveDbChanges(CancellationToken cancellationToken = default(CancellationToken));
}
