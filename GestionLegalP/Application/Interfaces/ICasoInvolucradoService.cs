using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface ICasoInvolucradoService
    {
        Task<List<CasoInvolucradoDto>> GetActivosAsync();
        Task<List<object>> GetTodosAsync();
        Task<CasoInvolucradoDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(CasoInvolucradoDto dto);
        Task<string> ActualizarAsync(string codigo, CasoInvolucradoDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}