using Microsoft.AspNetCore.Identity;

namespace LevantamientoDeRed.Entities
{
    public class UsuarioRol : IdentityUserRole<string>
    {
        public Rol? Rol { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
