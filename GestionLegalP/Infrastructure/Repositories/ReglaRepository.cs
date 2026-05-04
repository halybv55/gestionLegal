using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class ReglaRepository : IReglaRepository
    {
        private readonly GestionLegalPContext _context;

        public ReglaRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<Regla>> GetActivasAsync()
        {
            return await _context.Regla
                .Where(r => r.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<Regla>> GetTodasAsync()
        {
            return await _context.Regla.ToListAsync();
        }

        public async Task<Regla?> GetByCodigoAsync(string codigo)
        {
            return await _context.Regla
                .FirstOrDefaultAsync(r => r.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.Regla
                .AnyAsync(r => r.Codigo == codigo);
        }

        public async Task CrearAsync(Regla regla)
        {
            _context.Regla.Add(regla);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Regla regla)
        {
            _context.Regla.Update(regla);
            await _context.SaveChangesAsync();
        }
    }
}