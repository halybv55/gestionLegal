using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface ISolicitudRevisionRepository
    {
        Task<List<SolicitudRevision>> GetActivasAsync();
        Task<List<SolicitudRevision>> GetTodasAsync();
        Task<SolicitudRevision?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(SolicitudRevision revision);
        Task ActualizarAsync(SolicitudRevision revision);
    }
}