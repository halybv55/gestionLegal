using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface ISolicitudRepository
    {
        Task<List<Solicitud>> GetActivasAsync();
        Task<List<Solicitud>> GetTodasAsync();
        Task<Solicitud?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(Solicitud solicitud);
        Task ActualizarAsync(Solicitud solicitud);
    }
}