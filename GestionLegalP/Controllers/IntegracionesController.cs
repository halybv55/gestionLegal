using GestionLegalP.Application.DTOs.Externos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionLegalP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegracionesController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public IntegracionesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("rrhh/despidos")]
        public async Task<IActionResult> GetDespidosRRHH()
        {
            var url = "http://localhost:5205/api/MIS/despido";

            var despidos = await _httpClient.GetFromJsonAsync<List<DespidoRRHHDto>>(url);

            return Ok(despidos);
        }
    }
}
