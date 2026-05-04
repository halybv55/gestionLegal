using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Interfaces
{
    public interface IDocumentoDecisionRepository
    {
        Task<List<DocumentoDecision>> GetActivosAsync();
        Task<List<DocumentoDecision>> GetTodosAsync();
        Task<DocumentoDecision?> GetByCodigoAsync(string codigo);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task CrearAsync(DocumentoDecision decision);
        Task ActualizarAsync(DocumentoDecision decision);
    }
}