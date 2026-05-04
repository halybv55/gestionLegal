using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;

namespace GestionLegalP.Application.Services
{
    public class DocumentoDecisionService : IDocumentoDecisionService
    {
        private readonly IDocumentoDecisionRepository _repository;
        private readonly IDocumentoLegalRepository _documentoRepository;

        public DocumentoDecisionService(
            IDocumentoDecisionRepository repository,
            IDocumentoLegalRepository documentoRepository)
        {
            _repository = repository;
            _documentoRepository = documentoRepository;
        }

        public async Task<List<DocumentoDecisionDto>> GetActivosAsync()
        {
            var decisiones = await _repository.GetActivosAsync();
            return decisiones.Select(DocumentoDecisionMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodosAsync()
        {
            var decisiones = await _repository.GetTodosAsync();

            return decisiones.Select(d => new
            {
                d.Codigo,
                CodigoDocumentoLegal = d.DocumentoLegal.Codigo,
                d.TipoDecision,
                d.FechaDecision,
                d.Observacion,
                d.Estado
            }).Cast<object>().ToList();
        }

        public async Task<DocumentoDecisionDto?> GetByCodigoAsync(string codigo)
        {
            var decision = await _repository.GetByCodigoAsync(codigo);

            if (decision == null || decision.Estado != "Activo")
                return null;

            return DocumentoDecisionMapper.ToDto(decision);
        }

        public async Task<string> CrearAsync(DocumentoDecisionDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var documento = await _documentoRepository.GetByCodigoAsync(dto.CodigoDocumentoLegal);

            if (documento == null || documento.Estado != "Activo")
                return "Documento legal no encontrado o inactivo.";

            var decision = DocumentoDecisionMapper.ToEntity(dto, documento.Id_DocumentoLegal);

            await _repository.CrearAsync(decision);

            return "Decisión de documento creada correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, DocumentoDecisionDto dto)
        {
            var decision = await _repository.GetByCodigoAsync(codigo);

            if (decision == null || decision.Estado != "Activo")
                return "Decisión no encontrada o inactiva.";

            var documento = await _documentoRepository.GetByCodigoAsync(dto.CodigoDocumentoLegal);

            if (documento == null || documento.Estado != "Activo")
                return "Documento legal no encontrado o inactivo.";

            DocumentoDecisionMapper.UpdateEntity(decision, dto, documento.Id_DocumentoLegal);

            await _repository.ActualizarAsync(decision);

            return "Decisión de documento actualizada correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var decision = await _repository.GetByCodigoAsync(codigo);

            if (decision == null || decision.Estado != "Activo")
                return "Decisión no encontrada o ya está inactiva.";

            decision.Estado = "Inactivo";

            await _repository.ActualizarAsync(decision);

            return "Decisión de documento desactivada correctamente.";
        }
    }
}