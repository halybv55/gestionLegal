using GestionLegalP.Dominio;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly GestionLegalPContext _context;

        public SolicitudRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<Solicitud>> GetActivasAsync()
        {
            return await _context.Solicitud
                .Where(s => s.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<Solicitud>> GetTodasAsync()
        {
            return await _context.Solicitud.ToListAsync();
        }

        public async Task<Solicitud?> GetByCodigoAsync(string codigo)
        {
            return await _context.Solicitud
                .FirstOrDefaultAsync(s => s.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.Solicitud
                .AnyAsync(s => s.Codigo == codigo);
        }

        public async Task CrearAsync(Solicitud solicitud)
        {
            _context.Solicitud.Add(solicitud);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Solicitud solicitud)
        {
            _context.Solicitud.Update(solicitud);
            await _context.SaveChangesAsync();
        }
    }
}