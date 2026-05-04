using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface IConsentimientoDocumentoRepository
    {
        Task<List<ConsentimientoDocumento>> GetActivosAsync();
        Task<List<ConsentimientoDocumento>> GetTodosAsync();
        Task<ConsentimientoDocumento?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(ConsentimientoDocumento consentimientoDocumento);
        Task ActualizarAsync(ConsentimientoDocumento consentimientoDocumento);
    }
}