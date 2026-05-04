using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class CasoDocumentoRepository : ICasoDocumentoRepository
    {
        private readonly GestionLegalPContext _context;

        public CasoDocumentoRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<CasoDocumento>> GetActivosAsync()
        {
            return await _context.CasoDocumento
                .Include(cd => cd.CasoLegal)
                .Include(cd => cd.DocumentoLegal)
                .Where(cd => cd.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<CasoDocumento>> GetTodosAsync()
        {
            return await _context.CasoDocumento
                .Include(cd => cd.CasoLegal)
                .Include(cd => cd.DocumentoLegal)
                .ToListAsync();
        }

        public async Task<CasoDocumento?> GetByCodigoAsync(string codigo)
        {
            return await _context.CasoDocumento
                .Include(cd => cd.CasoLegal)
                .Include(cd => cd.DocumentoLegal)
                .FirstOrDefaultAsync(cd => cd.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.CasoDocumento
                .AnyAsync(cd => cd.Codigo == codigo);
        }

        public async Task CrearAsync(CasoDocumento cd)
        {
            _context.CasoDocumento.Add(cd);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(CasoDocumento cd)
        {
            _context.CasoDocumento.Update(cd);
            await _context.SaveChangesAsync();
        }
    }
}
