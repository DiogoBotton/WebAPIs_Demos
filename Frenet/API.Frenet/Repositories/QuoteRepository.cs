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
    public class QuoteRepository : BaseRepository<Quote>, IQuoteRepository
    {
        public QuoteRepository(FrenetContext ctx) : base(ctx)
        {
        }

        public async Task AddRangeAsync(List<Quote> quotes)
        {
            await _ctx.Quotes.AddRangeAsync(quotes);
        }

        public async Task<List<Quote>> GetAllQuotesByShippingId(int shippingId)
        {
            return await _ctx.Quotes.Where(x => x.ShippingId == shippingId).ToListAsync();
        }
    }
}
