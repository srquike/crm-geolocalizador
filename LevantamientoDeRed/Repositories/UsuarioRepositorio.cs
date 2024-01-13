using LevantamientoDeRed.Database;
using LevantamientoDeRed.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LevantamientoDeRed.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly ApplicationDbContext _contexto;

        public UsuarioRepositorio(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<Rol> roleManager, ApplicationDbContext contexto)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _contexto = contexto;
        }

        public async Task<SignInResult> Acceder(string nombre, string clave)
        {
            return await _signInManager.PasswordSignInAsync(nombre, clave, false, false);
        }

        public async Task<IdentityResult> AsignarClaims(Usuario usuario, List<Claim> claims)
        {
            return await _userManager.AddClaimsAsync(usuario, claims);
        }

        public async Task<IdentityResult> AsignarRol(Usuario usuario, string rol)
        {
            return await _userManager.AddToRoleAsync(usuario, rol);
        }

        public async Task<IdentityResult> CambiarClave(Usuario usuario, string clave)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            return await _userManager.ResetPasswordAsync(usuario, token, clave);
        }

        public async Task<IdentityResult> CambiarRol(Usuario usuario, string rol)
        {
            if (await _roleManager.RoleExistsAsync(rol))
            {
                if (!await _userManager.IsInRoleAsync(usuario, rol))
                {
                    var roles = await _userManager.GetRolesAsync(usuario);
                    await _userManager.RemoveFromRolesAsync(usuario, roles);

                    return await _userManager.AddToRoleAsync(usuario, rol);
                }

                throw new Exception($"El usuario ya est&aacute; asignado al rol {rol}");
            }

            throw new Exception($"El {rol} no existe.");
        }

        public async Task CerrarSesion()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Crear(Usuario usuario, string clave)
        {
            return await _userManager.CreateAsync(usuario, clave);
        }

        public async Task<IdentityResult> Editar(Usuario usuario)
        {
            return await _userManager.UpdateAsync(usuario);
        }

        public async Task<IdentityResult> Eliminar(Usuario usuario)
        {
            return await _userManager.DeleteAsync(usuario);
        }

        public async Task<Usuario> ObtenerPorId(string id)
        {
            return await _contexto.Users.Include(u => u.UsuarioRoles).ThenInclude(ur => ur.Rol).FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<List<Rol>> ObtenerRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            return await _contexto.Users.Include(u => u.UsuarioRoles).ThenInclude(ur => ur.Rol).Where(u => !u.UserName.Equals("000000000")).AsNoTracking().ToListAsync();
        }
    }
}
