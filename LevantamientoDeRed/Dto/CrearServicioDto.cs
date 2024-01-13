using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto
{
    public class CrearServicioDto
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = $"{nameof(Nombre)} del servicio es requerido")]
        
        public string Nombre { get; set; }
    }
}
