using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Permify_Proto_WebApi.interfaces;
using Permify_Proto_WebApi.Models;

namespace Permify_Proto_WebApi.Repositories
{
    public class MongoDbProtoRepository : IProtoRepository
    {
        private const string _databaseName = "proto";
        private const string _collectionName = "proto_data";
        private readonly IMongoCollection<Proto> _protoDataCollection;
        private readonly FilterDefinitionBuilder<Proto> _filterBuilder = Builders<Proto>.Filter;
        public MongoDbProtoRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
            _protoDataCollection = database.GetCollection<Proto>(_collectionName);
        }

        public async Task<Proto> AddProtoAsync(Proto proto)
        {
            await _protoDataCollection.InsertOneAsync(proto);
            return proto;
        }

        public async Task<List<Proto>> AddProtosAsync(List<Proto> protos)
        {
            await _protoDataCollection.InsertManyAsync(protos);
            return protos;
        }

        public async Task<IEnumerable<Proto>> GetAllProtosAsync()
        {
            return await _protoDataCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Proto> GetProtoByIdAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(proto => proto.Id, id);
            return await _protoDataCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateProtoAsync(Proto proto)
        {
            var filter = _filterBuilder.Eq(proto => proto.Id, proto.Id);
            await _protoDataCollection.ReplaceOneAsync(filter, proto);
        }

        public async Task DeleteProtoAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(proto => proto.Id, id);
            await _protoDataCollection.DeleteOneAsync(filter);
        }
    }
}