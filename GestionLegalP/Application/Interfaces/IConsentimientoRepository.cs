using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface IConsentimientoRepository
    {
        Task<List<Consentimiento>> GetActivosAsync();
        Task<List<Consentimiento>> GetTodosAsync();
        Task<Consentimiento?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(Consentimiento consentimiento);
        Task ActualizarAsync(Consentimiento consentimiento);
    }
}