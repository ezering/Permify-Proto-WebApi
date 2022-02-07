using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Permify_Proto_WebApi.Dtos;
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
        public IEnumerable<ProtoDto> Get()
        {
            return _protoRepository.GetAllProtos().Select(
                proto => proto.ToDto()
            );
        }

        [HttpGet("{id}")]
        public ActionResult<ProtoDto> Get(Guid id)
        {
            var proto = _protoRepository.GetProtoById(id);
            if (proto == null)
            {
                return NotFound();
            }
            return _protoRepository.GetProtoById(id).ToDto();
        }

        // [HttpPost]
        // public Proto Post([FromBody] Proto proto)
        // {
        //     return _protoRepository.AddProto(proto);
        // }
    }
}