using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Repositories
{
    public interface ICablesRepositorio
    {
        Task<Cable?> GetCableByIdAsync(string? cableId);
        Task<List<Cable>> GetCablesAsync();
    }
}
