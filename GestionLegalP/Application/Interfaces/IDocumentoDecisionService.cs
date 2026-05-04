using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface IDocumentoDecisionService
    {
        Task<List<DocumentoDecisionDto>> GetActivosAsync();
        Task<List<object>> GetTodosAsync();
        Task<DocumentoDecisionDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(DocumentoDecisionDto dto);
        Task<string> ActualizarAsync(string codigo, DocumentoDecisionDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}