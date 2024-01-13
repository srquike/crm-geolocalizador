using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Repositories
{
    public interface IRepositorio<TEntidad> where TEntidad : Base
    {
        void Crear(TEntidad entidad);
        void Eliminar(TEntidad entidad);
        void Actualizar(TEntidad entidad);
        Task<TEntidad?> ObtenerPorIdAsync(string id, string includeProperties = "", bool rastreo = false);
        Task<List<TEntidad>> ObtenerTodosAsync(bool rastreo = false, string includeProperties = "");
        void CrearVarios(ICollection<TEntidad> entidades);
    }
}
