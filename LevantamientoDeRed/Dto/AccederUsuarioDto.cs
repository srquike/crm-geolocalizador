using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace LevantamientoDeRed.Dto
{
    public class AccederUsuarioDto
    {
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "Ingrese un n&uacute;mero de DUI valido."), Required(ErrorMessage = "El n&uacute;mero de DUI del usuario es requerido.")]
        public string? NumeroDui { get; set; }

        [Required(ErrorMessage = "La contrase&ntilde;a para el usuario es requerida.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "La contrase&ntilde;a debe tener al menos 6 car&aacute;cteres y un m&aacute;ximo de 10.")]
        public string? Clave { get; set; }

        public override string ToString()
        {
            using var algoritmo = SHA512.Create();
            var duiHash = BitConverter.ToString(algoritmo.ComputeHash(Encoding.UTF8.GetBytes(NumeroDui))).Replace("-", "");

            return $"DUI: {duiHash}";
        }
    }
}
