using GestionLegalP.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasMISController : ControllerBase
    {
        private readonly GestionLegalPContext _context;

        public ConsultasMISController(GestionLegalPContext context)
        {
            _context = context;
        }

        // 1. JOIN: Casos con documentos
        [HttpGet("casos-con-documentos")]
        public async Task<IActionResult> CasosConDocumentos()
        {
            var query = await (
                from cd in _context.CasoDocumento
                join c in _context.CasoLegal on cd.Id_CasoLegal equals c.Id_CasoLegal
                join d in _context.DocumentoLegal on cd.Id_DocumentoLegal equals d.Id_DocumentoLegal
                where cd.Estado == "Activo" && c.Estado == "Activo" && d.Estado == "Activo"
                select new
                {
                    CodigoCaso = c.Codigo,
                    c.TipoCaso,
                    c.Descripcion,
                    CodigoDocumento = d.Codigo,
                    d.Titulo,
                    d.Tipo
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 2. GROUP BY + COUNT: documentos por caso
        [HttpGet("conteo-documentos-por-caso")]
        public async Task<IActionResult> ConteoDocumentosPorCaso()
        {
            var query = await (
                from cd in _context.CasoDocumento
                join c in _context.CasoLegal on cd.Id_CasoLegal equals c.Id_CasoLegal
                where cd.Estado == "Activo" && c.Estado == "Activo"
                group cd by new { c.Codigo, c.Descripcion } into grupo
                select new
                {
                    CodigoCaso = grupo.Key.Codigo,
                    DescripcionCaso = grupo.Key.Descripcion,
                    TotalDocumentos = grupo.Count()
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 3. GROUP BY + SUM: documentos por tipo
        [HttpGet("suma-documentos-por-tipo")]
        public async Task<IActionResult> SumaDocumentosPorTipo()
        {
            var query = await (
                from cd in _context.CasoDocumento
                join d in _context.DocumentoLegal on cd.Id_DocumentoLegal equals d.Id_DocumentoLegal
                where cd.Estado == "Activo" && d.Estado == "Activo"
                group cd by d.Tipo into grupo
                select new
                {
                    TipoDocumento = grupo.Key,
                    Total = grupo.Sum(x => 1)
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 4. Filtro por código
        [HttpGet("buscar-caso/{codigo}")]
        public async Task<IActionResult> BuscarCaso(string codigo)
        {
            var query = await (
                from c in _context.CasoLegal
                where c.Codigo == codigo && c.Estado == "Activo"
                select new
                {
                    c.Codigo,
                    c.TipoCaso,
                    c.Descripcion,
                    c.FechaApertura,
                    c.EstadoCaso,
                    c.Prioridad
                }
            ).FirstOrDefaultAsync();

            if (query == null)
                return NotFound("Caso legal no encontrado.");

            return Ok(query);
        }

        // 5. NOT EXISTS: documentos sin caso
        [HttpGet("documentos-sin-caso")]
        public async Task<IActionResult> DocumentosSinCaso()
        {
            var query = await (
                from d in _context.DocumentoLegal
                where d.Estado == "Activo"
                   && !(from cd in _context.CasoDocumento
                        where cd.Id_DocumentoLegal == d.Id_DocumentoLegal
                           && cd.Estado == "Activo"
                        select cd).Any()
                select new
                {
                    d.Codigo,
                    d.Titulo,
                    d.Tipo,
                    d.FechaEmision
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 6. Casos por prioridad
        [HttpGet("casos-por-prioridad")]
        public async Task<IActionResult> CasosPorPrioridad()
        {
            var query = await (
                from c in _context.CasoLegal
                where c.Estado == "Activo"
                group c by c.Prioridad into grupo
                select new
                {
                    Prioridad = grupo.Key,
                    TotalCasos = grupo.Count()
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 7. Casos por estado del caso
        [HttpGet("casos-por-estado-caso")]
        public async Task<IActionResult> CasosPorEstadoCaso()
        {
            var query = await (
                from c in _context.CasoLegal
                where c.Estado == "Activo"
                group c by c.EstadoCaso into grupo
                select new
                {
                    EstadoCaso = grupo.Key,
                    TotalCasos = grupo.Count()
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 8. Documentos por tipo
        [HttpGet("documentos-por-tipo/{tipo}")]
        public async Task<IActionResult> DocumentosPorTipo(string tipo)
        {
            var query = await (
                from d in _context.DocumentoLegal
                where d.Estado == "Activo" && d.Tipo == tipo
                select new
                {
                    d.Codigo,
                    d.Titulo,
                    d.Tipo,
                    d.FechaEmision,
                    d.Formato
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 9. Consentimientos firmados
        [HttpGet("consentimientos-firmados")]
        public async Task<IActionResult> ConsentimientosFirmados()
        {
            var query = await (
                from c in _context.Consentimiento
                where c.Estado == "Activo" && c.Firmado == true
                select new
                {
                    c.Codigo,
                    c.TipoTratamiento,
                    c.Descripcion,
                    c.Fecha,
                    c.NombreFirmante,
                    c.ParentescoFirmante
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 10. Consentimientos no firmados
        [HttpGet("consentimientos-no-firmados")]
        public async Task<IActionResult> ConsentimientosNoFirmados()
        {
            var query = await (
                from c in _context.Consentimiento
                where c.Estado == "Activo" && c.Firmado == false
                select new
                {
                    c.Codigo,
                    c.TipoTratamiento,
                    c.Descripcion,
                    c.Fecha
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 11. Solicitudes con revisiones
        [HttpGet("solicitudes-con-revisiones")]
        public async Task<IActionResult> SolicitudesConRevisiones()
        {
            var query = await (
                from sr in _context.SolicitudRevision
                join s in _context.Solicitud on sr.Id_Solicitud equals s.Id_Solicitud
                where sr.Estado == "Activo" && s.Estado == "Activo"
                select new
                {
                    CodigoSolicitud = s.Codigo,
                    s.TipoSolicitud,
                    s.Motivo,
                    CodigoRevision = sr.Codigo,
                    sr.Resultado,
                    sr.FechaRevision,
                    sr.Observaciones
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 12. Solicitudes por resultado de revisión
        [HttpGet("solicitudes-por-resultado")]
        public async Task<IActionResult> SolicitudesPorResultado()
        {
            var query = await (
                from sr in _context.SolicitudRevision
                join s in _context.Solicitud on sr.Id_Solicitud equals s.Id_Solicitud
                where sr.Estado == "Activo" && s.Estado == "Activo"
                group sr by sr.Resultado into grupo
                select new
                {
                    Resultado = grupo.Key,
                    TotalSolicitudes = grupo.Count()
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 13. Decisiones sobre documentos
        [HttpGet("decisiones-documentos")]
        public async Task<IActionResult> DecisionesDocumentos()
        {
            var query = await (
                from dd in _context.DocumentoDecision
                join d in _context.DocumentoLegal on dd.Id_DocumentoLegal equals d.Id_DocumentoLegal
                where dd.Estado == "Activo" && d.Estado == "Activo"
                select new
                {
                    CodigoDecision = dd.Codigo,
                    CodigoDocumento = d.Codigo,
                    d.Titulo,
                    d.Tipo,
                    dd.TipoDecision,
                    dd.FechaDecision,
                    dd.Observacion
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 14. Documentos con consentimiento
        [HttpGet("documentos-con-consentimiento")]
        public async Task<IActionResult> DocumentosConConsentimiento()
        {
            var query = await (
                from cd in _context.ConsentimientoDocumento
                join c in _context.Consentimiento on cd.Id_Consentimiento equals c.Id_Consentimiento
                join d in _context.DocumentoLegal on cd.Id_DocumentoLegal equals d.Id_DocumentoLegal
                where cd.Estado == "Activo" && c.Estado == "Activo" && d.Estado == "Activo"
                select new
                {
                    CodigoConsentimiento = c.Codigo,
                    c.TipoTratamiento,
                    CodigoDocumento = d.Codigo,
                    d.Titulo,
                    d.Tipo,
                    cd.FechaAsociacion
                }
            ).ToListAsync();

            return Ok(query);
        }

        // 15. Casos con involucrados
        [HttpGet("casos-con-involucrados")]
        public async Task<IActionResult> CasosConInvolucrados()
        {
            var query = await (
                from ci in _context.CasoInvolucrado
                join c in _context.CasoLegal on ci.Id_CasoLegal equals c.Id_CasoLegal
                where ci.Estado == "Activo" && c.Estado == "Activo"
                select new
                {
                    CodigoCaso = c.Codigo,
                    c.TipoCaso,
                    c.Descripcion,
                    CodigoInvolucrado = ci.Codigo,
                    ci.RolInvolucrado,
                    ci.DescripcionParticipacion
                }
            ).ToListAsync();

            return Ok(query);
        }

        //Resultado de una solicitud
        [HttpGet("resultado-solicitud/{codigoSolicitud}")]
        public async Task<IActionResult> ResultadoSolicitud(string codigoSolicitud)
        {
            var query = await (
                from s in _context.Solicitud
                join sr in _context.SolicitudRevision
                    on s.Id_Solicitud equals sr.Id_Solicitud
                where s.Codigo == codigoSolicitud
                   && s.Estado == "Activo"
                   && sr.Estado == "Activo"
                orderby sr.FechaRevision descending
                select new
                {
                    CodigoSolicitud = s.Codigo,
                    s.TipoSolicitud,
                    s.Motivo,
                    Resultado = sr.Resultado,
                    sr.Observaciones,
                    sr.FechaRevision
                }
            ).FirstOrDefaultAsync();

            if (query == null)
                return NotFound("No hay revisión para esta solicitud.");

            return Ok(query);
        }

        //Estado de consentimiento
        [HttpGet("estado-consentimiento/{codigo}")]
        public async Task<IActionResult> EstadoConsentimiento(string codigo)
        {
            var query = await (
                from c in _context.Consentimiento
                where c.Codigo == codigo && c.Estado == "Activo"
                select new
                {
                    c.Codigo,
                    c.TipoTratamiento,
                    c.Firmado,
                    c.NombreFirmante,
                    c.ParentescoFirmante,
                    c.Fecha
                }
            ).FirstOrDefaultAsync();

            if (query == null)
                return NotFound("Consentimiento no encontrado.");

            return Ok(query);
        }

        //Documentos de un caso
        [HttpGet("documentos-de-caso/{codigoCaso}")]
        public async Task<IActionResult> DocumentosDeCaso(string codigoCaso)
        {
            var query = await (
                from c in _context.CasoLegal
                join cd in _context.CasoDocumento
                    on c.Id_CasoLegal equals cd.Id_CasoLegal
                join d in _context.DocumentoLegal
                    on cd.Id_DocumentoLegal equals d.Id_DocumentoLegal
                where c.Codigo == codigoCaso
                   && c.Estado == "Activo"
                   && cd.Estado == "Activo"
                   && d.Estado == "Activo"
                select new
                {
                    CodigoCaso = c.Codigo,
                    CodigoDocumento = d.Codigo,
                    d.Titulo,
                    d.Tipo,
                    d.FechaEmision
                }
            ).ToListAsync();

            return Ok(query);
        }
    }
}