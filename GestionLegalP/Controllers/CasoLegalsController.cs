using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.DTOs;
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
    public class CasoLegalsController : ControllerBase
    {
        private readonly ICasoLegalService _service;

        public CasoLegalsController(ICasoLegalService service)
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
        public async Task<IActionResult> GetCasoLegal(string codigo)
        {
            var caso = await _service.GetByCodigoAsync(codigo);

            if (caso == null)
                return NotFound("Caso legal no encontrado o inactivo.");

            return Ok(caso);
        }

        [HttpPost]
        public async Task<IActionResult> PostCasoLegal([FromQuery] CasoLegalDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutCasoLegal(string codigo, [FromQuery] CasoLegalDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Caso legal no encontrado o inactivo.")
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteCasoLegal(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Caso legal no encontrado o ya está inactivo.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
