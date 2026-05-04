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
    public class CasoInvolucradoesController : ControllerBase
    {
        private readonly ICasoInvolucradoService _service;

        public CasoInvolucradoesController(ICasoInvolucradoService service)
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
        public async Task<IActionResult> GetCasoInvolucrado(string codigo)
        {
            var involucrado = await _service.GetByCodigoAsync(codigo);

            if (involucrado == null)
                return NotFound("Involucrado no encontrado o inactivo.");

            return Ok(involucrado);
        }

        [HttpPost]
        public async Task<IActionResult> PostCasoInvolucrado([FromQuery] CasoInvolucradoDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe." ||
                resultado == "Caso legal no encontrado o inactivo.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutCasoInvolucrado(string codigo, [FromQuery] CasoInvolucradoDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Involucrado no encontrado o inactivo.")
                return NotFound(resultado);

            if (resultado == "Caso legal no encontrado o inactivo.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteCasoInvolucrado(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Involucrado no encontrado o ya está inactivo.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
