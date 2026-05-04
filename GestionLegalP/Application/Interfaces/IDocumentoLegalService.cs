using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface IDocumentoLegalService
    {
        Task<List<DocumentoLegalDto>> GetActivosAsync();
        Task<List<object>> GetTodosAsync();
        Task<DocumentoLegalDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(DocumentoLegalDto dto);
        Task<string> ActualizarAsync(string codigo, DocumentoLegalDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}