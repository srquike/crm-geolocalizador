using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto
{
    public class EditarUsuarioDto
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es requerido."), StringLength(15, MinimumLength = 3, ErrorMessage = "El nombre del usuario debe tener al menos 3 car&aacute;cteres y un m&aacute;ximo de 15.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del usuario es requerido."), StringLength(15, MinimumLength = 3, ErrorMessage = "El nombre del usuario debe tener al menos 3 car&aacute;cteres y un m&aacute;ximo de 15.")]
        public string? Apellido { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese un correo electr&oacute;nico valido.")]
        public string? Email { get; set; }
    }
}
