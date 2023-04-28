using API.Demo.MongoDB.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.MongoDB.SeedWork
{
    public interface IRepository<T> where T : AbstractDomain
    {
        T Create(T objeto);
        T GetById(string id);
        List<T> GetAll();
        void Update(T objeto);
    }
}
