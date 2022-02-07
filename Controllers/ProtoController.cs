using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Permify_Proto_WebApi.interfaces;
using Permify_Proto_WebApi.Models;
using Permify_Proto_WebApi.Repositories;

namespace Permify_Proto_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProtoController : ControllerBase
    {
        private readonly IProtoRepository _protoRepository;
        public ProtoController(IProtoRepository protoRepository)
        {
            _protoRepository = protoRepository;
        }

        [HttpGet]
        public IEnumerable<Proto> Get()
        {
            return _protoRepository.GetAllProtos();
        }
    }
}