using System.Linq.Expressions;

namespace Domains.SeedWork;

public interface IRepository<TEntity> where TEntity : AbstractDomain
{
    IUnitOfWork UnitOfWork { get; }

    public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task<bool> SoftDelete(Guid id, CancellationToken cancellationToken = default);
    public Task<TEntity> GetById(Guid Id, params Expression<Func<TEntity, object>>[] includeProperties);
    public IQueryable<TEntity> List(params Expression<Func<TEntity, object>>[] includeProperties);
    public IQueryable<TEntity> ListBy(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
}
