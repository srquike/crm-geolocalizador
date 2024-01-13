using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers
{
    public class CablesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger;

        public CablesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsuariosController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Eliminar(string? id)
        {
            try
            {
                if (id == null)
                {
                    ViewData["error_obtener_cable"] = "No fue posible obtener los datos del cable";
                    return View();
                }

                var cable = await _unitOfWork.CablesRepositorio.GetCableByIdAsync(id);

                if (cable == null)
                {
                    ViewData["error_obtener_cable"] = "No fue posible obtener los datos del cable";
                    return View();
                }

                var result = _mapper.Map<CableDto>(cable);

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos del cable: {Message}", ex.Message);
                ViewData["error_obtener_cable"] = "No fue posible obtener los datos del cable";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ConfirmarEliminar(string? id)
        {
            try
            {
                if (id == null)
                {
                    ViewData["error_obtener_cable"] = "No fue posible obtener los datos del cable";
                    return View();
                }

                var cable = await _unitOfWork.CablesRepositorio.GetCableByIdAsync(id);
                
                if (cable == null)
                {
                    ViewData["error_obtener_cable"] = "No fue posible obtener los datos del cable";
                    return View();
                }

                _unitOfWork.Repositorio<Cable>().Eliminar(cable);

                if (await _unitOfWork.SaveChangesAsync())
                    return View(nameof(Index));

                ViewData["error_obtener_cable"] = "No fue posible eliminar los datos del cable";
                return View(_mapper.Map<CableDto>(cable));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible eliminar o obtener los datos del cable: {Message}", ex.Message);
                ViewData["error_obtener_cable"] = "No fue posible eliminar o obtener los datos del cable";
                return View();
            }
        }
    }
}
