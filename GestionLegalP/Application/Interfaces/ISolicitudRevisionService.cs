using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface ISolicitudRevisionService
    {
        Task<List<SolicitudRevisionDto>> GetActivasAsync();
        Task<List<object>> GetTodasAsync();
        Task<SolicitudRevisionDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(SolicitudRevisionDto dto);
        Task<string> ActualizarAsync(string codigo, SolicitudRevisionDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}