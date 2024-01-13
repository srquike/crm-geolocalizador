namespace LevantamientoDeRed.Dto
{
    public class ContratoDto
    {
        public string Id{ get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioMensual { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFinal { get; set; }

        public string? ServicioId { get; set; }

        public string? Servicio { get; set; }
    }
}
