using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface IConsentimientoDocumentoService
    {
        Task<List<ConsentimientoDocumentoDto>> GetActivosAsync();
        Task<List<object>> GetTodosAsync();
        Task<ConsentimientoDocumentoDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(ConsentimientoDocumentoDto dto);
        Task<string> ActualizarAsync(string codigo, ConsentimientoDocumentoDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}