using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Interfaces
{
    public interface ISolicitudService
    {
        Task<List<SolicitudDto>> GetActivasAsync();
        Task<List<object>> GetTodasAsync();
        Task<SolicitudDto?> GetByCodigoAsync(string codigo);
        Task<string> CrearAsync(SolicitudDto dto);
        Task<string> ActualizarAsync(string codigo, SolicitudDto dto);
        Task<string> DesactivarAsync(string codigo);
    }
}