using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto
{
    public class CambiarRolUsuarioDto
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "El rol del usuario es requerido.")]
        public string? Rol { get; set; }
    }
}
