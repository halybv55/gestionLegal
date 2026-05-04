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
    public class ConsentimientoDocumentoesController : ControllerBase
    {
        private readonly IConsentimientoDocumentoService _service;

        public ConsentimientoDocumentoesController(IConsentimientoDocumentoService service)
        {
            _service = service;
        }

        [HttpGet("listaCompleta")]
        public async Task<IActionResult> GetListaCompleta()
        {
            return Ok(await _service.GetTodosAsync());
        }

        [HttpGet("listaActiva")]
        public async Task<IActionResult> GetListaActiva()
        {
            return Ok(await _service.GetActivosAsync());
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetConsentimientoDocumento(string codigo)
        {
            var relacion = await _service.GetByCodigoAsync(codigo);

            if (relacion == null)
                return NotFound("Relación no encontrada o inactiva.");

            return Ok(relacion);
        }

        [HttpPost]
        public async Task<IActionResult> PostConsentimientoDocumento([FromQuery] ConsentimientoDocumentoDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe." ||
                resultado == "Consentimiento no encontrado o inactivo." ||
                resultado == "Documento legal no encontrado o inactivo.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutConsentimientoDocumento(string codigo, [FromQuery] ConsentimientoDocumentoDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Relación no encontrada o inactiva.")
                return NotFound(resultado);

            if (resultado == "Consentimiento no encontrado o inactivo." ||
                resultado == "Documento legal no encontrado o inactivo.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteConsentimientoDocumento(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Relación no encontrada o ya está inactiva.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
