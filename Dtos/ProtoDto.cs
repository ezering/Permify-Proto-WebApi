using System;
using Permify_Proto_WebApi.Models;

namespace Permify_Proto_WebApi.Dtos
{
    public class ProtoDto
    {
        public Guid Id { get; set; }
        public string FormType { get; set; }

        public ShapeGeoData GeoJson { get; set; }
    }
}