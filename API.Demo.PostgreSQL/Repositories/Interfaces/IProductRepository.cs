using API.Demo.PostgreSQL.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.PostgreSQL.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        List<Product> GetAll();
    }
}
