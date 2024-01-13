using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto.Gpon
{
    public class CrearGponDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = $"{nameof(Nombre)} es requerido")]
        public string Nombre { get; set; }
        public string? PosteId { get; set; }
    }
}
