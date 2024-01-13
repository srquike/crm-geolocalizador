namespace LevantamientoDeRed.Dto
{
    public class CrearUbicacionDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double Longitud { get; set; }
        public double Latitud { get; set; }
    }
}