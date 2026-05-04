using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class DocumentoDecisionRepository : IDocumentoDecisionRepository
    {
        private readonly GestionLegalPContext _context;

        public DocumentoDecisionRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentoDecision>> GetActivosAsync()
        {
            return await _context.DocumentoDecision
                .Include(d => d.DocumentoLegal)
                .Where(d => d.Estado == "Activo" && d.DocumentoLegal.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<DocumentoDecision>> GetTodosAsync()
        {
            return await _context.DocumentoDecision
                .Include(d => d.DocumentoLegal)
                .ToListAsync();
        }

        public async Task<DocumentoDecision?> GetByCodigoAsync(string codigo)
        {
            return await _context.DocumentoDecision
                .Include(d => d.DocumentoLegal)
                .FirstOrDefaultAsync(d => d.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.DocumentoDecision
                .AnyAsync(d => d.Codigo == codigo);
        }

        public async Task CrearAsync(DocumentoDecision decision)
        {
            _context.DocumentoDecision.Add(decision);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(DocumentoDecision decision)
        {
            _context.DocumentoDecision.Update(decision);
            await _context.SaveChangesAsync();
        }
    }
}