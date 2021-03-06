using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Permify_Proto_WebApi.Dtos;
using Permify_Proto_WebApi.interfaces;
using Permify_Proto_WebApi.Models;

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
        public async Task<IEnumerable<ProtoDto>> Get()
        {
            return (await _protoRepository.GetAllProtosAsync()).Select(
                proto => proto.ToDto()
            );
        }

        [HttpPost("add-proto")]
        public async Task<ActionResult<ProtoDto>> Post(CreateProtoDto protoDto)
        {
            Proto proto = new()
            {
                Id = Guid.NewGuid(),
                FormType = protoDto.FormType,
                GeoJson = protoDto.GeoJson

            };

            await _protoRepository.AddProtoAsync(proto);
            return CreatedAtAction(nameof(GetProtoAsync), new { id = proto.Id }, proto.ToDto());
        }

        [HttpPost("add-protos")]
        public async Task<ActionResult<List<ProtoDto>>> Post(List<CreateProtoDto> protoDtos)
        {
            List<Proto> protos = new();
            foreach (var protoDto in protoDtos)
            {
                Proto proto = new()
                {
                    Id = Guid.NewGuid(),
                    FormType = protoDto.FormType,
                    GeoJson = protoDto.GeoJson
                };
                protos.Add(proto);
            }
            await _protoRepository.AddProtosAsync(protos);
            return CreatedAtAction(nameof(GetProtoAsync), new { id = protos.First().Id }, protos.Select(proto => proto.ToDto()));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProtoDto>> GetProtoAsync(Guid id)
        {
            var proto = await _protoRepository.GetProtoByIdAsync(id);
            if (proto == null)
            {
                return NotFound();
            }
            return (await _protoRepository.GetProtoByIdAsync(id)).ToDto();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ProtoDto>> Put(Guid id, UpdateProtoDto protoDto)
        {
            var proto = await _protoRepository.GetProtoByIdAsync(id);
            if (proto == null)
            {
                return NotFound();
            }
            proto.FormType = protoDto.FormType;
            proto.GeoJson = protoDto.GeoJson;
            await _protoRepository.UpdateProtoAsync(proto);
            return proto.ToDto();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProtoDto>> Delete(Guid id)
        {
            var proto = await _protoRepository.GetProtoByIdAsync(id);
            if (proto == null)
            {
                return NotFound();
            }
            await _protoRepository.DeleteProtoAsync(id);
            return proto.ToDto();
        }
    }
}