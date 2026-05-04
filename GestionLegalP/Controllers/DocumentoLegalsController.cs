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
    public class DocumentoLegalsController : ControllerBase
    {
        private readonly IDocumentoLegalService _service;

        public DocumentoLegalsController(IDocumentoLegalService service)
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
        public async Task<IActionResult> GetDocumentoLegal(string codigo)
        {
            var documento = await _service.GetByCodigoAsync(codigo);

            if (documento == null)
                return NotFound("Documento legal no encontrado o inactivo.");

            return Ok(documento);
        }

        [HttpPost]
        public async Task<IActionResult> PostDocumentoLegal([FromQuery] DocumentoLegalDto dto)
        {
            var resultado = await _service.CrearAsync(dto);

            if (resultado == "El código ya existe.")
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutDocumentoLegal(string codigo, [FromQuery] DocumentoLegalDto dto)
        {
            var resultado = await _service.ActualizarAsync(codigo, dto);

            if (resultado == "Documento no encontrado o inactivo.")
                return NotFound(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteDocumentoLegal(string codigo)
        {
            var resultado = await _service.DesactivarAsync(codigo);

            if (resultado == "Documento no encontrado o ya está inactivo.")
                return NotFound(resultado);

            return Ok(resultado);
        }
    }
}
