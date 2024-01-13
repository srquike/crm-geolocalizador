namespace LevantamientoDeRed.Dto
{
    public class CableDto
    {
        public string? Id { get; set; } 
        public string? Tipo { get; set; }
        public string? Estado { get; set; }

        public ICollection<PuntoDto>? Puntos { get; set; }
    }
}
