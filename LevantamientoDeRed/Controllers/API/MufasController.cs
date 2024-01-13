using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.API
{
    /// <summary>
    /// Endpoints para la API de Mufas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MufasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CablesController> _logger;

        /// <summary>
        /// Constructor principal para la API de Mufas
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public MufasController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CablesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint para la creacion de una mufa
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("crear")]
        public async Task<IActionResult> Create(CrearMufaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mufa = _mapper.Map<Mufa>(dto);

                if (!string.IsNullOrEmpty(dto.PosteId))
                    mufa.PosteId = dto.PosteId;

                if (!string.IsNullOrEmpty(dto.GponId))
                    mufa.GponId = dto.GponId;

                _unitOfWork.Repositorio<Mufa>().Crear(mufa);

                await _unitOfWork.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro de la mufa: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro de la mufa. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Endpoint para obtener una mufa por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(MufaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MufaDto>> Get(string id)
        {
            try
            {
                var mufa = await _unitOfWork.Repositorio<Mufa>().ObtenerPorIdAsync(id, "Gpon,Poste");

                if (mufa == null)
                    return NotFound();

                var result = _mapper.Map<MufaDto>(mufa);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro de la mufa: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro de la mufa. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Endpoint para obtener una lista de mufas
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<MufaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public async Task<ActionResult<List<MufaDto>>> Get()
        {
            try
            {
                var mufas = await _unitOfWork.Repositorio<Mufa>().ObtenerTodosAsync(includeProperties: "Gpon,Poste");
                var results = _mapper.Map<List<MufaDto>>(mufas);

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro de la mufa: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro de la mufa. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Endpoint para la modificación de una mufa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("editar")]
        public async Task<IActionResult> Edit(CrearMufaDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var mufa = _mapper.Map<Mufa>(dto);

                mufa.PosteId = string.IsNullOrEmpty(dto.PosteId) 
                    ? null 
                    : dto.PosteId;

                mufa.GponId = string.IsNullOrEmpty(dto.GponId) 
                    ? null 
                    : dto.GponId;

                _unitOfWork.Repositorio<Mufa>().Actualizar(mufa);

                await _unitOfWork.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro de la mufa: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro de la mufa. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }
    }
}
