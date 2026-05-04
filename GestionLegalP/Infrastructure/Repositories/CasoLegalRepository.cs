using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class CasoLegalRepository : ICasoLegalRepository
    {
        private readonly GestionLegalPContext _context;

        public CasoLegalRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<CasoLegal>> GetActivosAsync()
        {
            return await _context.CasoLegal
                .Where(c => c.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<CasoLegal>> GetTodosAsync()
        {
            return await _context.CasoLegal.ToListAsync();
        }

        public async Task<CasoLegal?> GetByCodigoAsync(string codigo)
        {
            return await _context.CasoLegal
                .FirstOrDefaultAsync(c => c.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.CasoLegal
                .AnyAsync(c => c.Codigo == codigo);
        }

        public async Task CrearAsync(CasoLegal casoLegal)
        {
            _context.CasoLegal.Add(casoLegal);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(CasoLegal casoLegal)
        {
            _context.CasoLegal.Update(casoLegal);
            await _context.SaveChangesAsync();
        }
    }
}