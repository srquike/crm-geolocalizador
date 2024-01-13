using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.API
{
    /// <summary>
    /// Endpoints para la API de Servicios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CablesController> _logger;

        /// <summary>
        /// Constructor principal para la API de Servicios
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ServiciosController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CablesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint para la creacion de un Servicio
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CrearServicioDto dTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var servicio = _mapper.Map<Servicio>(dTO);

                _unitOfWork.Repositorio<Servicio>().Crear(servicio);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return StatusCode(StatusCodes.Status201Created);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del servicio: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint para obtener un Servicio por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ServicioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicioDto>> Get(string id)
        {
            try
            {
                var servicio = await _unitOfWork.Repositorio<Servicio>().ObtenerPorIdAsync(id, includeProperties: "Contratos");

                if (servicio == null)
                    return NotFound();

                var result = _mapper.Map<ServicioDto>(servicio);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos del servicio: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint para obtener una lista de Servicios
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ServicioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet()]
        public async Task<ActionResult<List<ServicioDto>>> Get()
        {
            try
            {
                var servicios = await _unitOfWork.Repositorio<Servicio>().ObtenerTodosAsync(includeProperties: "Contratos");
                var results = _mapper.Map<List<ServicioDto>>(servicios);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener la lista de servicios: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
