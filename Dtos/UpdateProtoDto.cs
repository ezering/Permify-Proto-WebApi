using System.ComponentModel.DataAnnotations;
using Permify_Proto_WebApi.Models;

namespace Permify_Proto_WebApi.Dtos
{
    public class UpdateProtoDto
    {
        [Required]
        public string FormType { get; set; }

        [Required]
        public ShapeGeoData GeoJson { get; set; }
    }
}