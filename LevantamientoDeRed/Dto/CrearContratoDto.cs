using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto
{
    public class CrearContratoDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = $"{nameof(Nombre)} del Contrato es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = $"{nameof(Descripcion)} del contrato es requerido")]
        public string Descripcion { get; set; }
        
        public double PrecioMensual { get; set; }
       
        public DateTimeOffset FechaInicio { get; set; }
     
        public DateTimeOffset FechaFinal { get; set; }
        
        public string ServicioId { get; set; }
    }
}
