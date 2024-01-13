using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Repositories
{
    public interface IUnitOfWork
    {
        IUsuarioRepositorio UsuarioRepositorio { get; }
        ICablesRepositorio CablesRepositorio { get; }
        IPosteRepositorio PosteRepositorio { get; }

        IRepositorio<TEntidad> Repositorio<TEntidad>() where TEntidad : Base;
        Task<bool> SaveChangesAsync();
    }
}
