namespace LevantamientoDeRed.Dto
{
    public class PosteDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public string? MufaId { get; set; }
        public string? GponId { get; set; }
    }
}
