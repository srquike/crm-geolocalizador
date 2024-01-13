using LevantamientoDeRed.Database;
using LevantamientoDeRed.Entities;
using Microsoft.EntityFrameworkCore;

namespace LevantamientoDeRed.Repositories
{
    public class Repositorio<TEntidad> : IRepositorio<TEntidad> where TEntidad : Base
    {
        private readonly ApplicationDbContext _contexto;
        private readonly DbSet<TEntidad> _entidades;

        public Repositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
            _entidades = _contexto.Set<TEntidad>();
        }

        public void Actualizar(TEntidad entidad)
        {
            _entidades.Update(entidad);
        }

        public void Crear(TEntidad entidad)
        {
            _entidades.Add(entidad);
        }

        public void CrearVarios(ICollection<TEntidad> entidades)
        {
            _entidades.AddRange(entidades);
        }

        public void Eliminar(TEntidad entidad)
        {
            _entidades.Remove(entidad);
        }

        public async Task<TEntidad?> ObtenerPorIdAsync(string id, string includeProperties = "", bool rastreo = false)
        {
            if (string.IsNullOrEmpty(includeProperties)) 
            {
                return await _entidades.FirstOrDefaultAsync(e => e.Id.Equals(id));
            }

            IQueryable<TEntidad> query = _entidades;

            foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<List<TEntidad>> ObtenerTodosAsync(bool rastreo = false, string includeProperties = "")
        {
            if (string.IsNullOrEmpty(includeProperties)) 
            {
                return await _entidades.ToListAsync();
            }

            IQueryable<TEntidad> query = _entidades;

            foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }
    }
}
