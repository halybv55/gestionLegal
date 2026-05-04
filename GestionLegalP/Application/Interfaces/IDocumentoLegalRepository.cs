using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface IDocumentoLegalRepository
    {
        Task<List<DocumentoLegal>> GetActivosAsync();
        Task<List<DocumentoLegal>> GetTodosAsync();
        Task<DocumentoLegal?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(DocumentoLegal doc);
        Task ActualizarAsync(DocumentoLegal doc);
    }
}