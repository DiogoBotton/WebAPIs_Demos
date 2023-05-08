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

        protected BaseRepository(FrenetContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateAsync(T objeto)
        {
            await _ctx.Set<T>().AddAsync(objeto);
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync() =>
            await _ctx.Set<T>().ToListAsync();
    }
}
