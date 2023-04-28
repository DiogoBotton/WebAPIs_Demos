using API.Demo.MongoDB.Context;
using API.Demo.MongoDB.Domains;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.MongoDB.SeedWork
{
    public abstract class BaseRepository<T> : IRepository<T> where T : AbstractDomain
    {
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collectionName;
        private readonly string _databaseName;
        public BaseRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, IMongoSettings mongoSettings, string collectionName)
        {
            (_mongoClient, _clientSessionHandle, _databaseName, _collectionName) = (mongoClient, clientSessionHandle, mongoSettings.DatabaseName, collectionName);

            // Cria collection caso não exista no database
            if (!_mongoClient.GetDatabase(_databaseName).ListCollectionNames().ToList().Contains(_collectionName))
                _mongoClient.GetDatabase(_databaseName).CreateCollection(_collectionName);
        }

        protected virtual IMongoCollection<T> Collection => _mongoClient.GetDatabase(_databaseName).GetCollection<T>(_collectionName);

        public T Create(T objeto)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(T objeto)
        {
            throw new NotImplementedException();
        }
    }
}
