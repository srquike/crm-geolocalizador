using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Repositories
{
    public interface IPosteRepositorio
    {
        Task<Poste?> GetPosteById(string id, bool rastreable);
        Task<List<Poste>> GetPostes(bool rastreable);
    }
}
