namespace LevantamientoDeRed.Dto
{
    public class ClienteDto
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public string? MufaId { get; set; }
        public string? ContratoId { get; set; }
    }
}
