using API.Demo.MongoDB.Context;
using API.Demo.MongoDB.Domains;
using API.Demo.MongoDB.Repositories.Interfaces;
using API.Demo.MongoDB.SeedWork;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.MongoDB.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(
            IMongoClient mongoClient, 
            IClientSessionHandle clientSessionHandle,
            IMongoSettings mongoSettings) 
            : base(mongoClient, clientSessionHandle, mongoSettings, mongoSettings.ProductCollectionName)
        {
        }

        // ProductRepository herda de BaseRepository a propriedade "Collection" para acessar a coleção específica para consultas customizadas
        public async Task<Product> GetById(string id) =>
            await Collection.Find(f => f.Id == id).FirstOrDefaultAsync();
    }
}
