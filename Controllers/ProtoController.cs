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
        public ActionResult<ProtoDto> GetProto(Guid id)
        {
            var proto = _protoRepository.GetProtoById(id);
            if (proto == null)
            {
                return NotFound();
            }
            return _protoRepository.GetProtoById(id).ToDto();
        }

        [HttpPost("add-proto")]
        public ActionResult<ProtoDto> Post(CreateProtoDto protoDto)
        {
            Proto proto = new()
            {
                Id = Guid.NewGuid(),
                FormType = protoDto.FormType,
                GeoData = protoDto.GeoJson

            };

            _protoRepository.AddProto(proto);
            return CreatedAtAction(nameof(GetProto), new { id = proto.Id }, proto.ToDto());
        }

        [HttpPost("add-protos")]
        public ActionResult<List<ProtoDto>> Post(List<CreateProtoDto> protoDtos)
        {
            List<Proto> protos = new();
            foreach (var protoDto in protoDtos)
            {
                Proto proto = new()
                {
                    Id = Guid.NewGuid(),
                    FormType = protoDto.FormType,
                    GeoData = protoDto.GeoJson
                };
                protos.Add(proto);
            }
            _protoRepository.AddProtos(protos);
            return CreatedAtAction(nameof(GetProto), new { id = protos.First().Id }, protos.Select(proto => proto.ToDto()));
        }

        [HttpPut("{id}")]
        public ActionResult<ProtoDto> Put(Guid id, UpdateProtoDto protoDto)
        {
            var proto = _protoRepository.GetProtoById(id);
            if (proto == null)
            {
                return NotFound();
            }
            proto.FormType = protoDto.FormType;
            proto.GeoData = protoDto.GeoJson;
            _protoRepository.UpdateProto(proto);
            return proto.ToDto();
        }

        [HttpDelete("{id}")]
        public ActionResult<ProtoDto> Delete(Guid id)
        {
            var proto = _protoRepository.GetProtoById(id);
            if (proto == null)
            {
                return NotFound();
            }
            _protoRepository.DeleteProto(id);
            return proto.ToDto();
        }
    }
}