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
    public class DocumentoDecisionsController : ControllerBase
    {
        private readonly IDocumentoDecisionService _service;

        public DocumentoDecisionsController(IDocumentoDecisionService service)
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
        public async Task<IActionResult> GetDocumentoDecision(string codigo)
        {
            var decision = await _service.GetByCodigoAsync(codigo);

            if (decision == null)
                return NotFound("Decisión no encontrada o inactiva.");

            return Ok(decision);
        }

        [HttpPost]
        public async Task<IActionResult> PostDocumentoDecision([FromQuery] DocumentoDecisionDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe." ||
                resultado == "Documento legal no encontrado o inactivo.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutDocumentoDecision(string codigo, [FromQuery] DocumentoDecisionDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Decisión no encontrada o inactiva.")
                return NotFound(resultado);

            if (resultado == "Documento legal no encontrado o inactivo.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteDocumentoDecision(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Decisión no encontrada o ya está inactiva.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
