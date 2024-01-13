using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LevantamientoDeRed.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiciosController> _logger;

        public ServiciosController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ServiciosController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: ServiciosController
        public async Task<ActionResult> Index()
        {
            try
            {
                var servicios = await _unitOfWork.Repositorio<Servicio>().ObtenerTodosAsync(includeProperties: "Contratos");
                var resultados = _mapper.Map<List<ServicioDto>>(servicios);

                return View(resultados);
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_listado_servicios"] = $"No fue posible obtener el listado de servicios: {ex.Message}";
                return View();
            }
        }

        // GET: ServiciosController/Create
        public ActionResult Crear()
        {
            var servicio = new CrearServicioDto();

            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(CrearServicioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_crear"] = "No fue posible crear el registro del nuevo servicio";
                    return View(dto);
                }

                var servicio = _mapper.Map<Servicio>(dto);
                _unitOfWork.Repositorio<Servicio>().Crear(servicio);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return RedirectToAction(nameof(Index));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro de Servicio: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult> Editar(string id)
        {
            try
            {
                var usuario = await _unitOfWork.Repositorio<Servicio>().ObtenerPorIdAsync(id, includeProperties: "Contratos");

                if (usuario is null)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible obtener los datos del usuario.";
                    return View();
                }

                var resultado = _mapper.Map<CrearServicioDto>(usuario);

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
        public async Task<ActionResult> Editar(CrearServicioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible editar el registro del Servicio: Los datos no son validos";
                    return View(dto);
                }

                var entidad = await _unitOfWork.Repositorio<Servicio>().ObtenerPorIdAsync(dto.Id, includeProperties: "Contratos");

                if (entidad is null)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible editar el registro del Servicio.";
                    return View(dto);
                }

                var servicio = _mapper.Map(dto, entidad);

                _unitOfWork.Repositorio<Servicio>().Actualizar(servicio);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("Index", "Servicios");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_editar"] = $"No fue posible editar el registro del servicio: {ex.Message}";
                return View();
            }
        }
       

        public async Task<ActionResult> Eliminar(string id)
        {
            try
            {
                var servicio = await _unitOfWork.Repositorio<Servicio>().ObtenerPorIdAsync(id);

                if (servicio is null)
                {
                    ViewData["error_mensaje_eliminar"] = "No fue posible obtener los datos del servicio.";
                    return View();
                }

                var resultado = _mapper.Map<ServicioDto>(servicio);

                return View(resultado);
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_eliminar"] = $"No fue posible eliminar el registro del servicio: {ex.Message}";
                return View();
            }
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarEliminar(string id)
        {
            try
            {
                var servicio = await _unitOfWork.Repositorio<Servicio>().ObtenerPorIdAsync(id);

                if (servicio is null)
                {
                    ViewData["error_mensaje_eliminar"] = "No fue posible eliminar el registro del servicio";
                    return RedirectToAction("Index", "Servicios");
                }

                _unitOfWork.Repositorio<Servicio>().Eliminar(servicio);
                if (await _unitOfWork.SaveChangesAsync())
                {
                    return RedirectToAction(nameof(Index));
                }


                return RedirectToAction("Index", "Servicios");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_eliminar"] = $"No fue posible eliminar el registro del usuario: {ex.Message}";
                return RedirectToAction("Index", "Servicios");
            }
        }

        [ActionName("ServiciosObtener")]
        public async Task<IActionResult> ObtenerTodosServicios()
        {
            return Json(await _unitOfWork.Repositorio<Servicio>().ObtenerTodosAsync());
        }


    }
}
