using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface ICasoLegalService
    {
        Task<List<CasoLegalDto>> GetActivosAsync();
        Task<List<object>> GetTodosAsync();
        Task<CasoLegalDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(CasoLegalDto dto);
        Task<string> ActualizarAsync(string codigo, CasoLegalDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}