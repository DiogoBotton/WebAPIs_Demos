using API.Frenet.Context;
using API.Frenet.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.SeedWork
{
    public abstract class BaseRepository<T> : IRepository<T> where T : AbstractDomain
    {
        protected FrenetContext _ctx;
        private DbSet<T> _entity;
        IUnitOfWork IRepository<T>.UnitOfWork => _ctx;

        protected BaseRepository(FrenetContext ctx)
        {
            _ctx = ctx;
            _entity = _ctx.Set<T>();
        }

        public async Task<T> CreateAsync(T objeto)
        {
            return await Task.FromResult(_entity.AddAsync(objeto).Result.Entity);
        }

        public async Task<List<T>> GetAllAsync() =>
            await _entity.ToListAsync();
    }
}
