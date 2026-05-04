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
    public class ConsentimientoesController : ControllerBase
    {
        private readonly IConsentimientoService _service;

        public ConsentimientoesController(IConsentimientoService service)
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
        public async Task<IActionResult> GetConsentimiento(string codigo)
        {
            var consentimiento = await _service.GetByCodigoAsync(codigo);

            if (consentimiento == null)
                return NotFound("Consentimiento no encontrado o inactivo.");

            return Ok(consentimiento);
        }

        [HttpPost]
        public async Task<IActionResult> PostConsentimiento([FromQuery] ConsentimientoDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutConsentimiento(string codigo, [FromQuery] ConsentimientoDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Consentimiento no encontrado o inactivo.")
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteConsentimiento(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Consentimiento no encontrado o ya está inactivo.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
