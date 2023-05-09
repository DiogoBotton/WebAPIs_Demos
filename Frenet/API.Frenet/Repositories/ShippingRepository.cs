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
    public class ShippingRepository : BaseRepository<Shipping>, IShippingRepository
    {
        public ShippingRepository(FrenetContext context) : base(context)
        {
        }

        public async Task<List<Shipping>> GetAllByCEP(string cep)
        {
            return await _ctx.Shippings.Where(x => x.RecipientCEP == cep).ToListAsync();
        }
    }
}
