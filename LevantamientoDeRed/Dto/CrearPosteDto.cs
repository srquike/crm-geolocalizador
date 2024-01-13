using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto
{
    public class CrearPosteDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = $"{nameof(Nombre)} es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = $"{nameof(Longitud)} es requerido.")]
        public double Longitud { get; set; }

        [Required(ErrorMessage = $"{nameof(Latitud)} es requerido.")]
        public double Latitud { get; set; }

        public string? MufaId { get; set; }
        public string? GponId { get; set; }
    }
}
