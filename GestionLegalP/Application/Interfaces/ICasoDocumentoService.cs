using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface ICasoDocumentoService
    {
        Task<List<CasoDocumentoDto>> GetActivosAsync();
        Task<List<object>> GetTodosAsync();
        Task<CasoDocumentoDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(CasoDocumentoDto dto);
        Task<string> ActualizarAsync(string codigo, CasoDocumentoDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}