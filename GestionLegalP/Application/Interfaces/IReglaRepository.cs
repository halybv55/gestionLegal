using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface IReglaRepository
    {
        Task<List<Regla>> GetActivasAsync();
        Task<List<Regla>> GetTodasAsync();
        Task<Regla?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(Regla regla);
        Task ActualizarAsync(Regla regla);
    }
}