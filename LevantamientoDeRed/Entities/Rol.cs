using Microsoft.AspNetCore.Identity;

namespace LevantamientoDeRed.Entities
{
    public class Rol : IdentityRole
    {
        public virtual ICollection<UsuarioRol>? UsuarioRoles { get; set; }
    }
}
