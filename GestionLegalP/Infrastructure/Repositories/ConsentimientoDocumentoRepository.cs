using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class ConsentimientoDocumentoRepository : IConsentimientoDocumentoRepository
    {
        private readonly GestionLegalPContext _context;

        public ConsentimientoDocumentoRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<ConsentimientoDocumento>> GetActivosAsync()
        {
            return await _context.ConsentimientoDocumento
                .Include(cd => cd.Consentimiento)
                .Include(cd => cd.DocumentoLegal)
                .Where(cd => cd.Estado == "Activo"
                    && cd.Consentimiento.Estado == "Activo"
                    && cd.DocumentoLegal.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<ConsentimientoDocumento>> GetTodosAsync()
        {
            return await _context.ConsentimientoDocumento
                .Include(cd => cd.Consentimiento)
                .Include(cd => cd.DocumentoLegal)
                .ToListAsync();
        }

        public async Task<ConsentimientoDocumento?> GetByCodigoAsync(string codigo)
        {
            return await _context.ConsentimientoDocumento
                .Include(cd => cd.Consentimiento)
                .Include(cd => cd.DocumentoLegal)
                .FirstOrDefaultAsync(cd => cd.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.ConsentimientoDocumento
                .AnyAsync(cd => cd.Codigo == codigo);
        }

        public async Task CrearAsync(ConsentimientoDocumento consentimientoDocumento)
        {
            _context.ConsentimientoDocumento.Add(consentimientoDocumento);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(ConsentimientoDocumento consentimientoDocumento)
        {
            _context.ConsentimientoDocumento.Update(consentimientoDocumento);
            await _context.SaveChangesAsync();
        }
    }
}