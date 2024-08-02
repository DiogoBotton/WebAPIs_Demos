using Domains;
using Domains.SeedWork;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Base;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : AbstractDomain
{
    protected ApiDbContext _context;
    private DbSet<TEntity> _entity;

    IUnitOfWork IRepository<TEntity>.UnitOfWork => _context;

    protected BaseRepository(ApiDbContext context)
    {
        _context = context;
        _entity = _context.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_entity.AddAsync(entity, cancellationToken).Result.Entity);
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        try
        {
            await Task.FromResult(_entity.Update(entity));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SoftDelete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            TEntity entity = await _entity
                .FirstOrDefaultAsync(x => x.Id == id);

            entity.DeletedAt = DateTime.Now;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public IQueryable<TEntity> List(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _entity
            .OnlyActives();

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return query;
    }

    public IQueryable<TEntity> ListBy(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
    => List(includeProperties).Where(where);
}
