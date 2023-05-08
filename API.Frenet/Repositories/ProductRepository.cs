using API.Frenet.Context;
using API.Frenet.Domains;
using API.Frenet.Repositories.Interfaces;
using API.Frenet.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(FrenetContext context): base(context)
        {
        }

        public Task<Product> GetByCEP(string cep)
        {
            return _ctx.Products.FirstOrDefaultAsync(x => x.RecipientCEP == cep);
        }
    }
}
