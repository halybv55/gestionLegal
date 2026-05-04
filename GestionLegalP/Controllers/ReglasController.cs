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
    public class ReglasController : ControllerBase
    {
        private readonly IReglaService _service;

        public ReglasController(IReglaService service)
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
        public async Task<IActionResult> GetRegla(string codigo)
        {
            var regla = await _service.GetByCodigoAsync(codigo);

            if (regla == null)
                return NotFound("Regla no encontrada o inactiva.");

            return Ok(regla);
        }

        [HttpPost]
        public async Task<IActionResult> PostRegla([FromQuery] ReglaDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutRegla(string codigo, [FromQuery] ReglaDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Regla no encontrada o inactiva.")
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteRegla(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Regla no encontrada o ya está inactiva.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
