using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Dto.Gpon;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.API
{
    /// <summary>
    /// Endpoints para la API de GPONs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GponsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CablesController> _logger;

        /// <summary>
        /// Constructor principal para la API de GPONs
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public GponsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CablesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint para la creacion de un GPON
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("crear")]
        public async Task<IActionResult> Create(CrearGponDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var gpon = _mapper.Map<Gpon>(dto);
                gpon.PosteId = string.IsNullOrEmpty(dto.PosteId) ? null : dto.PosteId;

                _unitOfWork.Repositorio<Gpon>().Crear(gpon);

                await _unitOfWork.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del GPON: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro del GPON. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }
        
        /// <summary>
        /// Endpoint para la modificación de un GPON
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("editar")]
        public async Task<IActionResult> Edit(CrearGponDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var gpon = _mapper.Map<Gpon>(dto);
                gpon.PosteId = string.IsNullOrEmpty(dto.PosteId) ? null : dto.PosteId;

                _unitOfWork.Repositorio<Gpon>().Actualizar(gpon);

                await _unitOfWork.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible modificar el registro del GPON: {Message}", ex.Message);
                return BadRequest($"No fue posible modificar el registro del GPON. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Endpoint para obtener un GPON por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(MufaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MufaDto>> Get(string id)
        {
            try
            {
                var gpon = await _unitOfWork.Repositorio<Gpon>().ObtenerPorIdAsync(id, includeProperties: "Poste");

                if (gpon == null)
                    return NotFound();

                var result = _mapper.Map<GponDto>(gpon);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del GPON: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro del GPON. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Endpoint para obtener una lista de GPONs
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<GponDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<GponDto>), StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public async Task<ActionResult<List<GponDto>>> Get()
        {
            try
            {
                var gpons = await _unitOfWork.Repositorio<Gpon>().ObtenerTodosAsync(includeProperties: "Poste");
                var results = _mapper.Map<List<GponDto>>(gpons);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del GPON: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro del GPON. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }
    }
}
