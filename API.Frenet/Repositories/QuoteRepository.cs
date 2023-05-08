using API.Frenet.Context;
using API.Frenet.Domains;
using API.Frenet.Repositories.Interfaces;
using API.Frenet.SeedWork;
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
    }
}
