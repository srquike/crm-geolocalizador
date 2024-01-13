namespace LevantamientoDeRed.Dto
{
    public class CrearPuntoDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public int Order { get; set; }
    }
}