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
    public class SolicitudsController : ControllerBase
    {
        private readonly ISolicitudService _service;

        public SolicitudsController(ISolicitudService service)
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
        public async Task<IActionResult> GetSolicitud(string codigo)
        {
            var solicitud = await _service.GetByCodigoAsync(codigo);

            if (solicitud == null)
                return NotFound("Solicitud no encontrada o inactiva.");

            return Ok(solicitud);
        }

        [HttpPost]
        public async Task<IActionResult> PostSolicitud([FromQuery] SolicitudDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutSolicitud(string codigo, [FromQuery] SolicitudDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Solicitud no encontrada o inactiva.")
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteSolicitud(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Solicitud no encontrada o ya está inactiva.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
