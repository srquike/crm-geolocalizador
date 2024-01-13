using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ClientesController> logger)
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
                    ViewData["error_obtener_cliente"] = "No fue posible obtener los datos del cliente";
                    return View();
                }

                var poste = await _unitOfWork.Repositorio<Cliente>().ObtenerPorIdAsync(id, rastreo: false);

                if (poste == null)
                {
                    ViewData["error_obtener_cliente"] = "No fue posible obtener los datos del cliente";
                    return View();
                }

                var result = _mapper.Map<ClienteDto>(poste);

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos del poste: {Message}", ex.Message);
                ViewData["error_obtener_cliente"] = "No fue posible obtener los datos del cliente";
                return View();
            }
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
                    ViewData["error_obtener_cliente"] = "No fue posible obtener los datos del cliente";
                    return View();
                }

                var cliente = await _unitOfWork.Repositorio<Cliente>().ObtenerPorIdAsync(id, rastreo: false);

                if (cliente == null)
                {
                    ViewData["error_obtener_cliente"] = "No fue posible obtener los datos del cliente";
                    return View();
                }

                _unitOfWork.Repositorio<Cliente>().Eliminar(cliente);

                if (await _unitOfWork.SaveChangesAsync())
                    return View(nameof(Index));

                ViewData["error_obtener_cliente"] = "No fue posible eliminar los datos del cliente";
                return View(_mapper.Map<ClienteDto>(cliente));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible eliminar o obtener los datos del cliente: {Message}", ex.Message);
                ViewData["error_obtener_cliente"] = "No fue posible eliminar o obtener los datos del cliente";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

