using System.ComponentModel.DataAnnotations;

namespace LevantamientoDeRed.Dto
{
    public class CrearMufaDTO
    {
        private string? posteId;
        private string? gponId;

        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = $"{nameof(Tipo)} es requerido.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = $"{nameof(Nombre)} es requerido.")]
        public string Nombre { get; set; }

        public string? PosteId { get => posteId; set => posteId = string.IsNullOrEmpty(value) ? null : value; }
        public string? GponId { get => gponId; set => gponId = string.IsNullOrEmpty(value) ? null : value; }
    }
}
