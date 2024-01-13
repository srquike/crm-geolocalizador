using Microsoft.AspNetCore.Identity;

namespace LevantamientoDeRed.Entities
{
    public class Accion:Base
    {
        public DateTime? Fecha { get; set; }
        public string? AccionRegistrada {  get; set; }
        public string? Tabla { get; set; }
        public string? UsuarioId { get; set;}

        public virtual Usuario? Usuario { get; set; }
    }
}
