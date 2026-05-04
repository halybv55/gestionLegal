using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;
using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Services
{
    public class DocumentoLegalService : IDocumentoLegalService
    {
        private readonly IDocumentoLegalRepository _repository;

        public DocumentoLegalService(IDocumentoLegalRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DocumentoLegalDto>> GetActivosAsync()
        {
            var docs = await _repository.GetActivosAsync();
            return docs.Select(DocumentoLegalMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodosAsync()
        {
            var docs = await _repository.GetTodosAsync();

            return docs.Select(d => new
            {
                d.Codigo,
                d.Titulo,
                d.Tipo,
                d.Descripcion,
                d.FechaEmision,
                d.Formato,
                d.ArchivoUrl,
                d.Estado
            }).Cast<object>().ToList();
        }

        public async Task<DocumentoLegalDto?> GetByCodigoAsync(string codigo)
        {
            var doc = await _repository.GetByCodigoAsync(codigo);

            if (doc == null || doc.Estado != "Activo")
                return null;

            return DocumentoLegalMapper.ToDto(doc);
        }

        public async Task<string> CrearAsync(DocumentoLegalDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var doc = DocumentoLegalMapper.ToEntity(dto);

            await _repository.CrearAsync(doc);

            return "Documento creado correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, DocumentoLegalDto dto)
        {
            var doc = await _repository.GetByCodigoAsync(codigo);

            if (doc == null || doc.Estado != "Activo")
                return "Documento no encontrado o inactivo.";

            DocumentoLegalMapper.UpdateEntity(doc, dto);

            await _repository.ActualizarAsync(doc);

            return "Documento actualizado correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var doc = await _repository.GetByCodigoAsync(codigo);

            if (doc == null || doc.Estado != "Activo")
                return "Documento no encontrado o ya está inactivo.";

            doc.Estado = "Inactivo";

            await _repository.ActualizarAsync(doc);

            return "Documento desactivado correctamente.";
        }
    }
}