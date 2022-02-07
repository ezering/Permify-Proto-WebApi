using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Permify_Proto_WebApi.interfaces;
using Permify_Proto_WebApi.Models;

namespace Permify_Proto_WebApi.Repositories
{
    public class MongoDbProtoRepository: IProtoRepository
    {
        private const string _databaseName = "proto";
        private const string _collectionName = "proto_data";
        private readonly IMongoCollection<Proto> _protoDataCollection;
        public MongoDbProtoRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(_databaseName);
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

        public void DeleteProto(Guid id)
        {
            _protoDataCollection.DeleteOne(proto => proto.Id == id);
        }

        public IEnumerable<Proto> GetAllProtos()
        {
            return _protoDataCollection.Find(proto => true).ToList();
        }

        public Proto GetProtoById(Guid id)
        {
            return _protoDataCollection.Find(proto => proto.Id == id).FirstOrDefault();
        }

        public void UpdateProto(Guid id)
        {
            _protoDataCollection.ReplaceOne(proto => proto.Id == id, GetProtoById(id));
        }
    }
}