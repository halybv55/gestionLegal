using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class DocumentoLegalRepository : IDocumentoLegalRepository
    {
        private readonly GestionLegalPContext _context;

        public DocumentoLegalRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentoLegal>> GetActivosAsync()
        {
            return await _context.DocumentoLegal
                .Where(d => d.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<DocumentoLegal>> GetTodosAsync()
        {
            return await _context.DocumentoLegal.ToListAsync();
        }

        public async Task<DocumentoLegal?> GetByCodigoAsync(string codigo)
        {
            return await _context.DocumentoLegal
                .FirstOrDefaultAsync(d => d.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.DocumentoLegal
                .AnyAsync(d => d.Codigo == codigo);
        }

        public async Task CrearAsync(DocumentoLegal doc)
        {
            _context.DocumentoLegal.Add(doc);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(DocumentoLegal doc)
        {
            _context.DocumentoLegal.Update(doc);
            await _context.SaveChangesAsync();
        }
    }
}