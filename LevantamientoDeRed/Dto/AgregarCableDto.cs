namespace LevantamientoDeRed.Dto
{
    public class AgregarCableDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Tipo { get; set; }
        public string? Estado { get; set; }

        public ICollection<CrearPuntoDto>? Puntos { get; set; }
    }
}
