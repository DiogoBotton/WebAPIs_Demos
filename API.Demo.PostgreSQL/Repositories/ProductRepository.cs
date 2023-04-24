using API.Demo.PostgreSQL.Context;
using API.Demo.PostgreSQL.Domains;
using API.Demo.PostgreSQL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.PostgreSQL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductContext _ctx;

        public ProductRepository(ProductContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Product> AddProduct(Product product)
        {
            var productDb = await _ctx.Products.AddAsync(product);

            await _ctx.SaveChangesAsync();

            return await Task.FromResult(productDb.Entity);
        }

        public List<Product> GetAll()
        {
            return _ctx.Products.ToList();
        }
    }
}
