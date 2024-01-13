namespace LevantamientoDeRed.Entities
{
    public class Contrato: Base
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioMensual { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFinal { get; set; }       
        public string? ServicioId { get; set; }

        public Servicio? Servicio { get; set; }

        public ICollection<Cliente> Cliente { get; set; }
    }
}
