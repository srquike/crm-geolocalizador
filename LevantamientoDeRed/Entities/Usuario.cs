using Microsoft.AspNetCore.Identity;

namespace LevantamientoDeRed.Entities
{
    public class Usuario : IdentityUser
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public virtual ICollection<IdentityUserClaim<string>>? Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>>? Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>>? Tokens { get; set; }
        public virtual ICollection<UsuarioRol>? UsuarioRoles { get; set; }
        public virtual ICollection<Accion>? Acciones { get; set; }
    }
}
