﻿using API.Frenet.Domains;
using API.Frenet.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Repositories.Interfaces
{
    public interface IQuoteRepository : IRepository<Quote>
    {
        Task AddRangeAsync(List<Quote> quotes);
        Task<List<Quote>> GetAllQuotesByShippingId(int shippingId);
    }
}
