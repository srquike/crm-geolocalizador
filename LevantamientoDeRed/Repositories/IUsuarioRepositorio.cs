using LevantamientoDeRed.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LevantamientoDeRed.Repositories
{
    public interface IUsuarioRepositorio
    {
        Task<IdentityResult> Crear(Usuario usuario, string clave);
        Task<SignInResult> Acceder(string nombre, string clave);
        Task<IdentityResult> Eliminar(Usuario usuario);
        Task<IdentityResult> Editar(Usuario usuario);
        Task<List<Rol>> ObtenerRoles();
        Task<List<Usuario>> ObtenerUsuarios();
        Task<IdentityResult> AsignarRol(Usuario usuario, string rol);
        Task<IdentityResult> AsignarClaims(Usuario usuario, List<Claim> claims);
        Task CerrarSesion();
        Task<Usuario> ObtenerPorId(string id);
        Task<IdentityResult> CambiarClave(Usuario usuario, string clave);
        Task<IdentityResult> CambiarRol(Usuario usuario, string rol);
    }
}
