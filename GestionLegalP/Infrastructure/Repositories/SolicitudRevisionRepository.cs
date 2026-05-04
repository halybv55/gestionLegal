using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class SolicitudRevisionRepository : ISolicitudRevisionRepository
    {
        private readonly GestionLegalPContext _context;

        public SolicitudRevisionRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<SolicitudRevision>> GetActivasAsync()
        {
            return await _context.SolicitudRevision
                .Include(sr => sr.Solicitud)
                .Where(sr => sr.Estado == "Activo" && sr.Solicitud.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<SolicitudRevision>> GetTodasAsync()
        {
            return await _context.SolicitudRevision
                .Include(sr => sr.Solicitud)
                .ToListAsync();
        }

        public async Task<SolicitudRevision?> GetByCodigoAsync(string codigo)
        {
            return await _context.SolicitudRevision
                .Include(sr => sr.Solicitud)
                .FirstOrDefaultAsync(sr => sr.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.SolicitudRevision
                .AnyAsync(sr => sr.Codigo == codigo);
        }

        public async Task CrearAsync(SolicitudRevision revision)
        {
            _context.SolicitudRevision.Add(revision);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(SolicitudRevision revision)
        {
            _context.SolicitudRevision.Update(revision);
            await _context.SaveChangesAsync();
        }
    }
}