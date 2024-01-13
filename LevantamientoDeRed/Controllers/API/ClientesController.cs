using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Dto.Cliente;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.API
{
    /// <summary>
    /// Endpoints para la API de Clientes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientesController> _logger;

        /// <summary>
        /// Constructor principal para la API de Clientes
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ClientesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ClientesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint para la creacion de un Cliente
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("crear")]
        public async Task<IActionResult> Create(CrearClienteDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cliente = _mapper.Map<Cliente>(dto);
                cliente.MufaId = string.IsNullOrEmpty(dto.MufaId) ? null : dto.MufaId;
                cliente.ContratoId = string.IsNullOrEmpty(dto.ContratoId) ? null : dto.ContratoId;

                _unitOfWork.Repositorio<Cliente>().Crear(cliente);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return StatusCode(StatusCodes.Status201Created);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del cliente: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        /// <summary>
        /// Endpoint para la modificación de un Cliente
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("editar")]
        public async Task<IActionResult> Edit(CrearClienteDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cliente = _mapper.Map<Cliente>(dto);

                cliente.MufaId = string.IsNullOrEmpty(dto.MufaId) ? null : dto.MufaId;
                cliente.ContratoId = string.IsNullOrEmpty(dto.ContratoId) ? null : dto.ContratoId;

                _unitOfWork.Repositorio<Cliente>().Actualizar(cliente);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return StatusCode(StatusCodes.Status201Created);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del cliente: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint para obtener una lista de Clientes
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ClienteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> Get()
        {
            try
            {
                var clientes = await _unitOfWork.Repositorio<Cliente>().ObtenerTodosAsync(includeProperties: "Mufa,Contrato");
                var results = _mapper.Map<List<ClienteDto>>(clientes);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener la lista de clientes: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint para obtener un Cliente por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> Get(string id)
        {
            try
            {
                var cliente = await _unitOfWork.Repositorio<Cliente>().ObtenerPorIdAsync(id, includeProperties: "Mufa");

                if (cliente == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                var result = _mapper.Map<ClienteDto>(cliente);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos del cliente: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
