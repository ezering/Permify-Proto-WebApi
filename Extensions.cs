using Permify_Proto_WebApi.Dtos;
using Permify_Proto_WebApi.Models;

namespace Permify_Proto_WebApi
{
    public static class Extensions
    {
        public static ProtoDto ToDto(this Proto proto)
        {
            return new ProtoDto
            {
                Id = proto.Id,
                FormType = proto.FormType,
                GeoData = proto.GeoData
            };
        }
    }
}