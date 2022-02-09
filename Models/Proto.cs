using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Permify_Proto_WebApi.Models
{
    public class Proto
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement]
        public string FormType { get; set; }

        [BsonElement("geodata")]
        public ShapeGeoData GeoJson { get; set; }
    }
}