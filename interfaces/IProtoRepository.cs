using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Permify_Proto_WebApi.Models;

namespace Permify_Proto_WebApi.interfaces
{
    public interface IProtoRepository
    {
        Task<IEnumerable<Proto>> GetAllProtosAsync();

        Task<Proto> GetProtoByIdAsync(Guid id);

        Task<Proto> AddProtoAsync(Proto proto);

        Task<List<Proto>> AddProtosAsync(List<Proto> protos);

        Task UpdateProtoAsync(Proto proto);

        Task DeleteProtoAsync(Guid id);
    }
}