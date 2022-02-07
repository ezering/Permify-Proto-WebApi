using System.Collections.Generic;

namespace Permify_Proto_WebApi.Models
{
    public class ShapeGeoData
    {
        public string Type { get; set; }
        public List<List<List<double>>> Coordinates { get; set; }
    }
}