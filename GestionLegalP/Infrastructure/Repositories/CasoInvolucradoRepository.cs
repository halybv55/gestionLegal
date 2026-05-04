using GestionLegalP.Application.Interfaces;
using GestionLegalP.Dominio;
using GestionLegalP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionLegalP.Infrastructure.Repositories
{
    public class CasoInvolucradoRepository : ICasoInvolucradoRepository
    {
        private readonly GestionLegalPContext _context;

        public CasoInvolucradoRepository(GestionLegalPContext context)
        {
            _context = context;
        }

        public async Task<List<CasoInvolucrado>> GetActivosAsync()
        {
            return await _context.CasoInvolucrado
                .Include(ci => ci.CasoLegal)
                .Where(ci => ci.Estado == "Activo" && ci.CasoLegal.Estado == "Activo")
                .ToListAsync();
        }

        public async Task<List<CasoInvolucrado>> GetTodosAsync()
        {
            return await _context.CasoInvolucrado
                .Include(ci => ci.CasoLegal)
                .ToListAsync();
        }

        public async Task<CasoInvolucrado?> GetByCodigoAsync(string codigo)
        {
            return await _context.CasoInvolucrado
                .Include(ci => ci.CasoLegal)
                .FirstOrDefaultAsync(ci => ci.Codigo == codigo);
        }

        public async Task<bool> ExisteCodigoAsync(string codigo)
        {
            return await _context.CasoInvolucrado
                .AnyAsync(ci => ci.Codigo == codigo);
        }

        public async Task CrearAsync(CasoInvolucrado involucrado)
        {
            _context.CasoInvolucrado.Add(involucrado);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(CasoInvolucrado involucrado)
        {
            _context.CasoInvolucrado.Update(involucrado);
            await _context.SaveChangesAsync();
        }
    }
}