using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace LevantamientoDeRed.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsuariosController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var usuarios = await _unitOfWork.UsuarioRepositorio.ObtenerUsuarios();
                var resultados = _mapper.Map<List<UsuarioDto>>(usuarios);

                return View(resultados);
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_listado_usuarios"] = $"No fue posible obtener el listado de usuarios: {ex.Message}";
                return View();
            }
        }

        public async Task<ActionResult> Salir()
        {
            try
            {
                await _unitOfWork.UsuarioRepositorio.CerrarSesion();
                return RedirectToAction("Acceder", "Usuarios");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_salir"] = $"No fue posible cerrar la sesi&oacute; del usuario: {ex.Message}";
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult Acceder()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Acceder(AccederUsuarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_acceder"] = "No fue posible acceder a la cuenta del usuario. Los datos no son validos";
                    return View(dto);
                }

                var resultado = await _unitOfWork.UsuarioRepositorio.Acceder(dto.NumeroDui, dto.Clave);

                if (!resultado.Succeeded)
                {
                    ViewData["error_mensaje_acceder"] = $"No fue posible acceder a la cuenta del usuario. " +
                        $"\nUsuario bloqueado: {resultado.IsLockedOut}" +
                        $"\nNo permitido: {resultado.IsNotAllowed}" +
                        $"\nRequiere autenticaci&oacute;n en dos factores: {resultado.RequiresTwoFactor}";

                    _logger.LogWarning("Inicio de sesion fallido. {LoginData}", dto);

                    return View(dto);
                }

                _logger.LogInformation("Inicio de sesion exitoso. {LoginData}", dto);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_acceder"] = $"No fue posible acceder a la cuenta del usuario: {ex.Message}";

                _logger.LogWarning("Inicio de sesion fallido. {LoginData}", dto);

                return View(dto);
            }
        }

        public ActionResult Registrar()
        {
            var usuario = new CrearUsuarioDto();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar(CrearUsuarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_crear"] = "No fue posible crear el registro del nuevo usuario";
                    return View(dto);
                }

                var usuario = _mapper.Map<Usuario>(dto);
                var resultadoCrear = await _unitOfWork.UsuarioRepositorio.Crear(usuario, dto.Clave);

                if (!resultadoCrear.Succeeded)
                {
                    ViewData["error_mensaje_crear"] = "No fue posible crear el registro del nuevo usuario";
                    ViewData["error_errors_crear"] = resultadoCrear.Errors;
                    return View(dto);
                }

                var resultadoAsignar = await _unitOfWork.UsuarioRepositorio.AsignarRol(usuario, dto.Rol);

                if (!resultadoAsignar.Succeeded)
                {
                    ViewData["error_mensaje_crear"] = $"No fue posible asignar el rol [{dto.Rol}] al usuario";
                    ViewData["error_errors_crear"] = resultadoAsignar.Errors;
                    return View(dto);
                }

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Actor, string.Concat(dto.Nombre, " ", dto.Apellido)),
                };

                var resultadoClaims = await _unitOfWork.UsuarioRepositorio.AsignarClaims(usuario, claims);

                if (!resultadoClaims.Succeeded)
                {
                    ViewData["error_mensaje_crear"] = $"No fue posible asignar los claims al usuario";
                    ViewData["error_errors_crear"] = resultadoAsignar.Errors;
                    return View(dto);
                }
                 
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_crear"] = $"No fue posible crear el registro del nuevo usuario: {ex.Message}";
                return View(dto);
            }
        }

        public async Task<ActionResult> Editar(string id)
        {
            try
            {
                var usuario = await _unitOfWork.UsuarioRepositorio.ObtenerPorId(id);

                if (usuario is null)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible obtener los datos del usuario.";
                    return View();
                }

                var resultado = _mapper.Map<EditarUsuarioDto>(usuario);

                return View(resultado);
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_editar"] = $"No fue posible obtener los datos del usuario: {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EditarUsuarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible editar el registro del nuevo usuario: Los datos no son validos";
                    return View(dto);
                }

                var entidad = await _unitOfWork.UsuarioRepositorio.ObtenerPorId(dto.Id);

                if (entidad is null)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible editar el registro del nuevo usuario.";
                    return View(dto);
                }

                var usuario = _mapper.Map(dto, entidad);

                var resultado = await _unitOfWork.UsuarioRepositorio.Editar(usuario);

                if (!resultado.Succeeded)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible editar el registro del nuevo usuario.";
                    ViewData["error_errors_editar"] = resultado.Errors;
                    return View(dto);
                }

                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_editar"] = $"No fue posible editar el registro del nuevo usuario: {ex.Message}";
                return View();
            }
        }

        public async Task<ActionResult> Eliminar(string id)
        {
            try
            {
                var usuario = await _unitOfWork.UsuarioRepositorio.ObtenerPorId(id);

                if (usuario is null)
                {
                    ViewData["error_mensaje_eliminar"] = "No fue posible obtener los datos del usuario.";
                    return View();
                }

                var resultado = _mapper.Map<UsuarioDto>(usuario);

                return View(resultado);
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_eliminar"] = $"No fue posible eliminar el registro del usuario: {ex.Message}";
                return View();
            }
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarEliminar(string id)
        {
            try
            {
                var usuario = await _unitOfWork.UsuarioRepositorio.ObtenerPorId(id);

                if (usuario is null)
                {
                    ViewData["error_mensaje_eliminar"] = "No fue posible eliminar el registro del usuario";
                    return RedirectToAction("Index", "Usuarios");
                }

                var resultado = await _unitOfWork.UsuarioRepositorio.Eliminar(usuario);

                if (!resultado.Succeeded)
                {
                    ViewData["error_mensaje_eliminar"] = "No fue posible eliminar el registro del usuario";
                    ViewData["error_errors_eliminar"] = resultado.Errors;
                    return RedirectToAction("Index", "Usuarios");
                }

                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_eliminar"] = $"No fue posible eliminar el registro del usuario: {ex.Message}";
                return RedirectToAction("Index", "Usuarios");
            }
        }

        [ActionName("Roles")]
        public async Task<IActionResult> ObtenerRoles()
        {
            return Json(await _unitOfWork.UsuarioRepositorio.ObtenerRoles());
        }

        public ActionResult CambiarClave(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewData["error_mensaje_cambiarclave"] = "No fue posible obtener los datos del usuario.";
                return View();
            }

            var dto = new CambiarClaveUsuarioDto()
            {
                Id = id
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CambiarClave(CambiarClaveUsuarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_cambiarclave"] = "No fue posible cambiar la contrase&ntilde;a del usuario: Los datos no son validos.";
                    return View(dto);
                }

                var usuario = await _unitOfWork.UsuarioRepositorio.ObtenerPorId(dto.Id);

                if (usuario is null)
                {
                    ViewData["error_mensaje_cambiarclave"] = "No fue posible cambiar la contrase&ntilde;a del usuario.";
                    return View(dto);
                }

                var resultado = await _unitOfWork.UsuarioRepositorio.CambiarClave(usuario, dto.Clave);

                if (!resultado.Succeeded)
                {
                    ViewData["error_mensaje_cambiarclave"] = "No fue posible cambiar la contrase&ntilde;a del usuario.";
                    ViewData["error_errores_cambiarclave"] = resultado.Errors;
                    return View(dto);
                }

                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_cambiarclave"] = $"No fue posible cambiar la contrase&ntilde;a del usuario: {ex.Message}";
                return View(dto);
            }
        }

        public ActionResult CambiarRol(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewData["error_mensaje_cambiarrol"] = "No fue posible obtener los datos del usuario.";
                return View();
            }

            var dto = new CambiarRolUsuarioDto()
            {
                Id = id
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CambiarRol(CambiarRolUsuarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_cambiarrol"] = "No fue posible cambiar el rol del usuario: Los datos no son validos.";
                    return View(dto);
                }

                var usuario = await _unitOfWork.UsuarioRepositorio.ObtenerPorId(dto.Id);

                if (usuario is null)
                {
                    ViewData["error_mensaje_cambiarrol"] = "No fue posible cambiar el rol del usuario.";
                    return View(dto);
                }

                var resultado = await _unitOfWork.UsuarioRepositorio.CambiarRol(usuario, dto.Rol);

                if (!resultado.Succeeded)
                {
                    ViewData["error_mensaje_cambiarrol"] = "No fue posible cambiar el rol del usuario.";
                    ViewData["error_errores_cambiarrol"] = resultado.Errors;
                    return View(dto);
                }

                return RedirectToAction("Index", "Usuarios");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_cambiarrol"] = $"No fue posible cambiar el rol del usuario: {ex.Message}";
                return View(dto);
            }
        }
    }
}
