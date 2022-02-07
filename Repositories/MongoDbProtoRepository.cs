using System;
using System.Collections.Generic;
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

        public Proto AddProto(Proto proto)
        {
            _protoDataCollection.InsertOne(proto);
            return proto;
        }

        public List<Proto> AddProtos(List<Proto> protos)
        {
            _protoDataCollection.InsertMany(protos);
            return protos;
        }

        public IEnumerable<Proto> GetAllProtos()
        {
            return _protoDataCollection.Find(new BsonDocument()).ToList();
        }

        public Proto GetProtoById(Guid id)
        {
            var filter = _filterBuilder.Eq(proto => proto.Id, id);
            return _protoDataCollection.Find(filter).FirstOrDefault();
        }

        public void UpdateProto(Proto proto)
        {
            var filter = _filterBuilder.Eq(proto => proto.Id, proto.Id);
            _protoDataCollection.ReplaceOne(filter, proto);
        }

        public void DeleteProto(Guid id)
        {
            var filter = _filterBuilder.Eq(proto => proto.Id, id);
            _protoDataCollection.DeleteOne(filter);
        }
    }
}