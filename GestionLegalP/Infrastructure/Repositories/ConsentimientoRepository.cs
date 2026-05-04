using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class ConsentimientoRepository : IConsentimientoRepository
    {
        private readonly GestionLegalPContext _context;

        public ConsentimientoRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<Consentimiento>> GetActivosAsync()
        {
            return await _context.Consentimiento
                .Where(c => c.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<Consentimiento>> GetTodosAsync()
        {
            return await _context.Consentimiento.ToListAsync();
        }

        public async Task<Consentimiento?> GetByCodigoAsync(string codigo)
        {
            return await _context.Consentimiento
                .FirstOrDefaultAsync(c => c.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.Consentimiento
                .AnyAsync(c => c.Codigo == codigo);
        }

        public async Task CrearAsync(Consentimiento consentimiento)
        {
            _context.Consentimiento.Add(consentimiento);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Consentimiento consentimiento)
        {
            _context.Consentimiento.Update(consentimiento);
            await _context.SaveChangesAsync();
        }
    }
}