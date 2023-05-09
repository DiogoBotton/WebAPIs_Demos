using API.Frenet.Domains;
using API.Frenet.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Repositories.Interfaces
{
    public interface IShippingRepository : IRepository<Shipping>
    {
        Task<List<Shipping>> GetAllByCEP(string cep);
    }
}
