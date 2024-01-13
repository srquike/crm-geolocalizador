using LevantamientoDeRed.Database;
using LevantamientoDeRed.Entities;
using Microsoft.EntityFrameworkCore;

namespace LevantamientoDeRed.Repositories
{
    public class PosteRepositorio : IPosteRepositorio
    {
        private readonly ApplicationDbContext _contexto;

        public PosteRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Poste?> GetPosteById(string id, bool rastreable = false)
        {
            return await _contexto.Postes!
                .Include(p => p.Gpons)
                .Include(p => p.Mufas)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Poste>> GetPostes(bool rastreable = false)
        {
            return await _contexto.Postes!
                .Include(p => p.Gpons)
                .Include(p => p.Mufas)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
