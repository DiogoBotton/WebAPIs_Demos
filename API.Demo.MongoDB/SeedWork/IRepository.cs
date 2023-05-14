using API.Demo.MongoDB.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.MongoDB.SeedWork
{
    public interface IRepository<T> where T : AbstractDomain
    {
        Task CreateAsync(T objeto);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(string id, T objeto);
        Task DeleteAsync(string id);
    }
}
