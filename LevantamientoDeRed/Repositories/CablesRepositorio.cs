using LevantamientoDeRed.Database;
using LevantamientoDeRed.Entities;
using Microsoft.EntityFrameworkCore;

namespace LevantamientoDeRed.Repositories
{
    public class CablesRepositorio : ICablesRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public CablesRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Cable>> GetCablesAsync()
        {
            return await _contexto.Cables!
                .Include(c => c.Puntos!.OrderBy(p => p.Order))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cable?> GetCableByIdAsync(string? cableId)
        {
            if (cableId == null)
                throw new NullReferenceException("El ID del cable no es valido");

            return await _contexto.Cables!
                .Include(c => c.Puntos!.OrderBy(p => p.Order))
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cableId);
        }
    }
}
