using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.API
{
    /// <summary>
    /// Endpoints para la API de Contratos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContratosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CablesController> _logger;

        /// <summary>
        /// Constructor principal para la API de Contratos
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ContratosController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CablesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint para la creacion de un Contrato
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CrearContratoDto dTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var contrato = _mapper.Map<Contrato>(dTO);

                _unitOfWork.Repositorio<Contrato>().Crear(contrato);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return StatusCode(StatusCodes.Status201Created);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del contrato: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint para obtener un Contrato por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ContratoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoDto>> Get(string id)
        {
            try
            {
                var contrato = await _unitOfWork.Repositorio<Contrato>().ObtenerPorIdAsync(id, includeProperties: "Clientes");

                if (contrato == null)
                    return NotFound();

                var result = _mapper.Map<ContratoDto>(contrato);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos de la mufa: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint para obtener una lista de Contratos
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ContratoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet()]
        public async Task<ActionResult<List<ContratoDto>>> Get()
        {
            try
            {
                var contratos = await _unitOfWork.Repositorio<Contrato>().ObtenerTodosAsync();
                var results = _mapper.Map<List<ContratoDto>>(contratos);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener la lista de Contratos: {Message}", ex.Message);
                return BadRequest($"No fue posible obtener la lista de Contratos. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }
    }
}
