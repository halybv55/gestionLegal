using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface ICasoInvolucradoRepository
    {
        Task<List<CasoInvolucrado>> GetActivosAsync();
        Task<List<CasoInvolucrado>> GetTodosAsync();
        Task<CasoInvolucrado?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(CasoInvolucrado involucrado);
        Task ActualizarAsync(CasoInvolucrado involucrado);
    }
}