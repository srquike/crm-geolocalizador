using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto
{
    public class CrearUsuarioDto
    {
        [Required(ErrorMessage = "El nombre del usuario es requerido."), StringLength(15, MinimumLength = 3, ErrorMessage = "El nombre del usuario debe tener al menos 3 car&aacute;cteres y un m&aacute;ximo de 15.")]
        public string? Nombre { get; set; }

        [RegularExpression("^[0-9]{9}$", ErrorMessage = "Ingrese un n&uacute;mero de DUI valido."), Required(ErrorMessage = "El n&uacute;mero de DUI del usuario es requerido.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "El apellido del usuario es requerido."), StringLength(15, MinimumLength = 3, ErrorMessage = "El nombre del usuario debe tener al menos 3 car&aacute;cteres y un m&aacute;ximo de 15.")]
        public string? Apellido { get; set; }

        //[Required(ErrorMessage = "El correo electr&oacute;nico del usuario es requerido.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electr&oacute;nico valido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El rol del usuario es requerido.")]
        public string? Rol { get; set; }

        [Required(ErrorMessage = "La contrase&ntilde;a para el usuario es requerida.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "La contrase&ntilde;a debe tener al menos 6 car&aacute;cteres y un m&aacute;ximo de 10.")]
        [RegularExpression("^(?=.*\\d)+(?=(.*\\W))*(?=.*[a-zA-Z])(?!.*\\s).{6,10}$", ErrorMessage = "Ingrese una contrase&ntilde;a valida.")]
        public string? Clave { get; set; }

        [Compare("Clave", ErrorMessage = "Las contrase&ntilde;as no son iguales.")]
        public string? ConfirmarClave { get; set; }

        public SelectList? Roles { get; set; }
    }
}