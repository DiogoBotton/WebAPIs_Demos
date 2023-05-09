using API.Frenet.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.SeedWork
{
    public interface IRepository<T> where T : AbstractDomain
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> CreateAsync(T objeto);
        //T CreateAsync(T objeto);
        Task<List<T>> GetAllAsync();
    }
}
