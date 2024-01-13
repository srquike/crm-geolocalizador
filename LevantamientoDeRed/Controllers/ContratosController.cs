using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers
{
    public class ContratosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ContratosController> _logger;

        public ContratosController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ContratosController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: ContratosController
        public async Task<ActionResult> Index()
        {
            try
            {
                var contratos = await _unitOfWork.Repositorio<Contrato>().ObtenerTodosAsync(includeProperties: "Servicio");
                var resultados = _mapper.Map<List<ContratoDto>>(contratos);

                return View(resultados);
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_listado_servicios"] = $"No fue posible obtener el listado de contratos: {ex.Message}";
                return View();
            }
        }

            // GET: ContratosController/Create
            public ActionResult Crear()
        {
            var contrato = new CrearContratoDto();

            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(CrearContratoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_crear"] = "No fue posible crear el registro del nuevo contrato";
                    return View(dto);
                }

                var contrato = _mapper.Map<Contrato>(dto);
                _unitOfWork.Repositorio<Contrato>().Crear(contrato);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return RedirectToAction(nameof(Index));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro de Contrato: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Obtener Lista de Servicios
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ServicioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<List<ServicioDto>>> ListaDeServicios()
        {
            try
            {
                var servicios = await _unitOfWork.Repositorio<Servicio>().ObtenerTodosAsync();
                var results = _mapper.Map<List<ServicioDto>>(servicios);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener las lista de servicios: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ActionResult> Eliminar(string id)
        {
            try
            {
                var contrato = await _unitOfWork.Repositorio<Contrato>().ObtenerPorIdAsync(id);

                if (contrato is null)
                {
                    ViewData["error_mensaje_eliminar"] = "No fue posible obtener los datos del contrato.";
                    return View();
                }

                var resultado = _mapper.Map<ContratoDto>(contrato);

                return View(resultado);
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_eliminar"] = $"No fue posible eliminar el registro del contrato: {ex.Message}";
                return View();
            }
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarEliminar(string id)
        {
            try
            {
                var contrato = await _unitOfWork.Repositorio<Contrato>().ObtenerPorIdAsync(id);

                if (contrato is null)
                {
                    ViewData["error_mensaje_eliminar"] = "No fue posible eliminar el registro del contrato";
                    return RedirectToAction("Index", "Contratos");
                }

                _unitOfWork.Repositorio<Contrato>().Eliminar(contrato);
                if (await _unitOfWork.SaveChangesAsync())
                {
                    return RedirectToAction(nameof(Index));
                }


                return RedirectToAction("Index", "Contratos");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_eliminar"] = $"No fue posible eliminar el registro del usuario: {ex.Message}";
                return RedirectToAction("Index", "Servicios");
            }
        }

        public async Task<ActionResult> Editar(string id)
        {
            try
            {
                var contrato = await _unitOfWork.Repositorio<Contrato>().ObtenerPorIdAsync(id, includeProperties: "Servicio");

                if (contrato is null)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible obtener los datos del usuario.";
                    return View();
                }

                var resultado = _mapper.Map<CrearContratoDto>(contrato);

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
        public async Task<ActionResult> Editar(CrearContratoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible editar el registro del Contrato: Los datos no son validos";
                    return View(dto);
                }

                var entidad = await _unitOfWork.Repositorio<Contrato>().ObtenerPorIdAsync(dto.Id);

                if (entidad is null)
                {
                    ViewData["error_mensaje_editar"] = "No fue posible editar el registro del Contrato.";
                    return View(dto);
                }

                var contrato = _mapper.Map(dto, entidad);

                _unitOfWork.Repositorio<Contrato>().Actualizar(contrato);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("Index", "Contratos");
            }
            catch (Exception ex)
            {
                ViewData["error_mensaje_editar"] = $"No fue posible editar el registro del contrato: {ex.Message}";
                return View();
            }
        }

    }
}
