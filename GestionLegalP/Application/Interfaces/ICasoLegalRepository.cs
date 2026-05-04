using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface ICasoLegalRepository
    {
        Task<List<CasoLegal>> GetActivosAsync();
        Task<List<CasoLegal>> GetTodosAsync();
        Task<CasoLegal?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(CasoLegal casoLegal);
        Task ActualizarAsync(CasoLegal casoLegal);
    }
}