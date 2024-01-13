using LevantamientoDeRed.Database;
using LevantamientoDeRed.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace LevantamientoDeRed.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _contexto;
        private bool _contextoFinalizado;

        private IUsuarioRepositorio? _usuarioRepositorio;
        private ICablesRepositorio? _cablesRepositorio;
        private IPosteRepositorio? _posteRepositorio;

        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<Rol> _roleManager;

        public IUsuarioRepositorio UsuarioRepositorio => _usuarioRepositorio ??= new UsuarioRepositorio(_userManager, _signInManager, _roleManager, _contexto);

        public ICablesRepositorio CablesRepositorio => _cablesRepositorio ??= new CablesRepositorio(_contexto);

        public IPosteRepositorio PosteRepositorio => _posteRepositorio ??= new PosteRepositorio(_contexto);

        public UnitOfWork(ApplicationDbContext contexto, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<Rol> roleManager)
        {
            _contexto = contexto;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_contextoFinalizado)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
            }

            _contextoFinalizado = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepositorio<TEntidad> Repositorio<TEntidad>() where TEntidad : Base
        {
            var entityName = typeof(TEntidad).Name;
            var repositories = new Hashtable();

            if (!repositories.ContainsKey(entityName))
            {
                var repositoryType = typeof(Repositorio<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntidad)), _contexto);
                repositories.Add(entityName, repositoryInstance);
            }

            return (IRepositorio<TEntidad>)repositories[entityName];
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _contexto.SaveChangesAsync()) > 0;
        }
    }
}
