using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface IConsentimientoService
    {
        Task<List<ConsentimientoDto>> GetActivosAsync();
        Task<List<object>> GetTodosAsync();
        Task<ConsentimientoDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(ConsentimientoDto dto);
        Task<string> ActualizarAsync(string codigo, ConsentimientoDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}