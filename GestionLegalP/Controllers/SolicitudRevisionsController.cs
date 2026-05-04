using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionLegalP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudRevisionsController : ControllerBase
    {
        private readonly ISolicitudRevisionService _service;

        public SolicitudRevisionsController(ISolicitudRevisionService service)
        {
            _service = service;
        }

        [HttpGet("listaCompleta")]
        public async Task<IActionResult> GetListaCompleta()
        {
            return Ok(await _service.GetTodasAsync());
        }

        [HttpGet("listaActiva")]
        public async Task<IActionResult> GetListaActiva()
        {
            return Ok(await _service.GetActivasAsync());
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetSolicitudRevision(string codigo)
        {
            var revision = await _service.GetByCodigoAsync(codigo);

            if (revision == null)
                return NotFound("Revisión no encontrada o inactiva.");

            return Ok(revision);
        }

        [HttpPost]
        public async Task<IActionResult> PostSolicitudRevision([FromQuery] SolicitudRevisionDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe." ||
                resultado == "Solicitud no encontrada o inactiva.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutSolicitudRevision(string codigo, [FromQuery] SolicitudRevisionDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Revisión no encontrada o inactiva.")
                return NotFound(resultado);

            if (resultado == "Solicitud no encontrada o inactiva.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteSolicitudRevision(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Revisión no encontrada o ya está inactiva.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
