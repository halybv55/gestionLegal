using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface ICasoDocumentoRepository
    {
        Task<List<CasoDocumento>> GetActivosAsync();
        Task<List<CasoDocumento>> GetTodosAsync();
        Task<CasoDocumento?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(CasoDocumento cd);
        Task ActualizarAsync(CasoDocumento cd);
    }
}