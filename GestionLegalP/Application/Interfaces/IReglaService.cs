using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface IReglaService
    {
        Task<List<ReglaDto>> GetActivasAsync();
        Task<List<object>> GetTodasAsync();
        Task<ReglaDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(ReglaDto dto);
        Task<string> ActualizarAsync(string codigo, ReglaDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}