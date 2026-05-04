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
    public class CasoDocumentoesController : ControllerBase
    {
        private readonly ICasoDocumentoService _service;

        public CasoDocumentoesController(ICasoDocumentoService service)
        {
            _service = service;
        }

        [HttpGet("listaActiva")]
        public async Task<IActionResult> Get()
            => Ok(await _service.GetActivosAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] CasoDocumentoDto dto)
            => Ok(await _service.CrearAsync(dto));

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
            => Ok(await _service.DesactivarAsync(codigo));
    }
}
