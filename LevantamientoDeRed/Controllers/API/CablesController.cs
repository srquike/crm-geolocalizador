using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.API
{
    /// <summary>
    /// Endpoints para la API de Cables
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CablesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CablesController> _logger;

        /// <summary>
        /// Constructor principal para la API de Cables
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public CablesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CablesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint para la creacion de un Cable
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("crear")]
        public async Task<IActionResult> Create(AgregarCableDto dto)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return BadRequest(ModelState);

                if (dto.Puntos?.Count <= 0)
                    return BadRequest("No se puede crear un cable sin puntos seleccionados.");

                var cable = _mapper.Map<Cable>(dto);
                _unitOfWork.Repositorio<Cable>().Crear(cable);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return StatusCode(StatusCodes.Status201Created);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del cable: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint para obtener una lista de Cables
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<CableDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<List<CableDto>>> ObtenerCables()
        {
            try
            {
                var cables = await _unitOfWork.CablesRepositorio.GetCablesAsync();
                var results = _mapper.Map<List<CableDto>>(cables);   
                return Ok(results);
            }            
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener la lista de cables: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint para obtener un Cable por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CableDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{cableId}")]
        public async Task<ActionResult<CableDto>> ObtenerPorId(string? cableId)
        {
            try
            {
                var cable = await _unitOfWork.CablesRepositorio.GetCableByIdAsync(cableId);

                if (cable == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                var result = _mapper.Map<CableDto>(cable);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos del cable: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
