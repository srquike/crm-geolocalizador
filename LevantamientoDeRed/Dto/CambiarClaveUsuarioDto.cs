using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto
{
    public class CambiarClaveUsuarioDto
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "La contrase&ntilde;a para el usuario es requerida.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "La contrase&ntilde;a debe tener al menos 6 car&aacute;cteres y un m&aacute;ximo de 10.")]
        [RegularExpression("^(?=.*\\d)+(?=(.*\\W))*(?=.*[a-zA-Z])(?!.*\\s).{6,10}$", ErrorMessage = "Ingrese una contrase&ntilde;a valida.")]
        public string? Clave { get; set; }

        [Compare("Clave", ErrorMessage = "Las contrase&ntilde;as no son iguales.")]
        public string? ConfirmarClave { get; set; }
    }
}
