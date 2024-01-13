using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto.Cliente
{
    public class CrearClienteDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = $"{nameof(Codigo)} es requerido")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = $"{nameof(Nombre)} es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = $"{nameof(Apellido)} es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = $"{nameof(Direccion)} es requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = $"{nameof(Telefono)} es requerido")]
        public string Telefono { get; set; }
        public bool Activo { get; set; } = false;
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public string? MufaId { get; set; }
        public string? ContratoId { get; set; }
    }
}
