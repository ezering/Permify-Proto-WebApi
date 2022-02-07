using System;
using System.Collections.Generic;
using Permify_Proto_WebApi.Models;

namespace Permify_Proto_WebApi.interfaces
{
    public interface IProtoRepository
    {
        IEnumerable<Proto> GetAllProtos();

        Proto GetProtoById(Guid id);

        Proto AddProto(Proto proto);

        List<Proto> AddProtos(List<Proto> protos);

        void UpdateProto(Proto proto);

        void DeleteProto(Guid id);
    }
}