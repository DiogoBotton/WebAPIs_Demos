using API.Demo.MongoDB.Domains;
using API.Demo.MongoDB.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.MongoDB.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
