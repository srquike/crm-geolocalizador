using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.MVC
{
    public class GponsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger;

        public GponsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsuariosController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Eliminar(string id)
        {
            try
            {
                if (id == null)
                {
                    ViewData["error_obtener"] = "No fue posible obtener los datos del GPON";
                    return View();
                }

                var gpon = await _unitOfWork.Repositorio<Gpon>().ObtenerPorIdAsync(id);

                if (gpon == null)
                {
                    ViewData["error_obtener"] = "No fue posible obtener los datos del GPON";
                    return View();
                }

                var result = _mapper.Map<GponDto>(gpon);

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos del GPON: {Message}", ex.Message);
                ViewData["error_obtener"] = "No fue posible obtener los datos del GPON";
                return View();
            }
        }

        [ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ConfirmarEliminar(string id)
        {
            try
            {
                if (id == null)
                {
                    ViewData["error_obtener"] = "No fue posible obtener los datos del GPON";
                    return View();
                }

                var gpon = await _unitOfWork.Repositorio<Gpon>().ObtenerPorIdAsync(id, rastreo: false);

                if (gpon == null)
                {
                    ViewData["error_obtener"] = "No fue posible obtener los datos del GPON";
                    return View();
                }

                _unitOfWork.Repositorio<Gpon>().Eliminar(gpon);

                if (await _unitOfWork.SaveChangesAsync())
                    return View(nameof(Index));

                ViewData["error_obtener"] = "No fue posible eliminar los datos del poste";
                return View(_mapper.Map<GponDto>(gpon));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible eliminar o obtener los datos del poste: {Message}", ex.Message);
                ViewData["error_obtener"] = "No fue posible eliminar o obtener los datos del poste";
                return View();
            }
        }
    }
}
