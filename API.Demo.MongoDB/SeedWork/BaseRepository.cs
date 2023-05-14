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
        /* _mongoClient : é a interface do cliente para o MongoDB. Com ele fazemos algumas operações no banco de dados.
         * 
         * _clientSessionHandle : é o identificador de interface para uma sessão do cliente. Portanto, se você iniciar uma transação, deverá passar o identificador ao fazer alguma operação.
         * *** OBS. Basicamente uma transação serve para garantir a integridade do banco de dados caso haja uma exceção no momento do update, insert ou delete.
         *          Por exemplo, quando é necessário inserir valores em duas tabelas/coleções e a inserção em uma delas falha, não devemos permitir a inserção em nenhuma das duas. Ou seja,
         *          ou todas as instruções são executadas com sucesso, ou nada é persistido no banco de dados.
         *          
         * _collectionName : é o nome da coleção utilizada pela classe Generic.
         * 
         * _databaseName : é a constante que representa o nome do nosso banco de dados.
        */
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly string _collectionName;
        private readonly string _databaseName;

        // Recebemos todos esses paramêtros por Injeção de Dependência e atribuímos as propriedades criadas acima
        public BaseRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, IMongoSettings mongoSettings, string collectionName)
        {
            (_mongoClient, _clientSessionHandle, _databaseName, _collectionName) = (mongoClient, clientSessionHandle, mongoSettings.DatabaseName, collectionName);

            // Cria coleção caso não exista no banco de dados
            if (!_mongoClient.GetDatabase(_databaseName).ListCollectionNames().ToList().Contains(_collectionName))
                _mongoClient.GetDatabase(_databaseName).CreateCollection(_collectionName);
        }

        // Adquire coleção específica do banco de dados a partir do parâmetro _collecionName
        protected virtual IMongoCollection<T> Collection => _mongoClient.GetDatabase(_databaseName).GetCollection<T>(_collectionName);

        // Implementação dos métodos CRUD com expressão Lambda (pois é possível serem feitos apenas com uma única linha)
        public async Task CreateAsync(T objeto) =>
            await Collection.InsertOneAsync(_clientSessionHandle, objeto);

        public async Task DeleteAsync(string id) =>
            await Collection.DeleteOneAsync(_clientSessionHandle, filter => filter.Id == id);

        public async Task<List<T>> GetAllAsync() =>
            await Collection.AsQueryable().ToListAsync();

        public async Task UpdateAsync(string id, T objeto) =>
            // Replace => Substitui (inteiramente) um único documento
            await Collection.ReplaceOneAsync(_clientSessionHandle, filter => filter.Id == id, objeto);
    }
}
