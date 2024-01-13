using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;

namespace LevantamientoDeRed.Controllers.API
{
    /// <summary>
    /// Endpoints para la API de Postes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostesController> _logger;

        /// <summary>
        /// Constructor principal para la API de Postes
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public PostesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PostesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint para la creacion de un Poste
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("crear")]
        public async Task<IActionResult> Create(CrearPosteDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var poste = _mapper.Map<Poste>(dto);

                _unitOfWork.Repositorio<Poste>().Crear(poste);

                await AgregarOEditarGpon(poste, dto.GponId);
                await AgregarOEditarMufa(poste, dto.MufaId);
                await _unitOfWork.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del poste: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro del poste. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Endpoint para la edicion de un Poste
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("editar")]
        public async Task<IActionResult> Edit(CrearPosteDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var posteEntity = await _unitOfWork.Repositorio<Poste>().ObtenerPorIdAsync(dto.Id, "Gpons,Mufas");

                if (posteEntity == null)
                {
                    ModelState.AddModelError("PosteId", "Los datos del poste no existen o no estan disponibles.");
                    return BadRequest(ModelState);
                }

                var poste = _mapper.Map(dto, posteEntity);

                _unitOfWork.Repositorio<Poste>().Actualizar(poste);

                await AgregarOEditarGpon(poste, dto.GponId);
                await AgregarOEditarMufa(poste, dto.MufaId);
                await _unitOfWork.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del poste: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro del poste. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Endpoint para obtener una lista de Postes
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<PosteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<PosteDto>>> ObtenerPostes()
        {
            try
            {
                var postes = await _unitOfWork.PosteRepositorio.GetPostes(false);
                var results = _mapper.Map<List<PosteDto>>(postes);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del poste: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro del poste. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Endpoint para obtener un Poste por su ID
        /// </summary>
        /// <param name="posteId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PosteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{posteId}")]
        public async Task<ActionResult<PosteDto>> ObtenerPoste(string posteId)
        {
            try
            {
                var poste = await _unitOfWork.PosteRepositorio.GetPosteById(posteId, false);

                if (poste == null)
                    return NotFound();

                var result = _mapper.Map<PosteDto>(poste);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible crear el registro del poste: {Message}", ex.Message);
                return BadRequest($"No fue posible crear el registro del poste. Puede usar el siguiente codigo {ex.HResult} para solicitar ayuda.");
            }
        }

        /// <summary>
        /// Agrega o edita la relacion 1 a 1 entre un GPON y un Poste
        /// </summary>
        /// <param name="poste"></param>
        /// <param name="gponId"></param>
        /// <returns></returns>
        private async Task AgregarOEditarGpon(Poste poste, string? gponId)
        {
            if (!string.IsNullOrEmpty(gponId))
            {
                var gpon = await _unitOfWork.Repositorio<Gpon>().ObtenerPorIdAsync(id: gponId, rastreo: true);

                if (gpon != null)
                {
                    gpon.PosteId = poste.Id;
                }
            }
            else
            {
                gponId = poste.Gpons.FirstOrDefault()?.Id;

                if (!string.IsNullOrEmpty(gponId))
                {
                    var gpon = await _unitOfWork.Repositorio<Gpon>().ObtenerPorIdAsync(id: gponId, rastreo: true);

                    if (gpon != null)
                    {
                        gpon.PosteId = null;
                    }
                }
            }
        }

        /// <summary>
        /// Agrega o edita la relacion 1 a 1 entre una Mufa y un Poste
        /// </summary>
        /// <param name="poste"></param>
        /// <param name="mufaId"></param>
        /// <returns></returns>
        private async Task AgregarOEditarMufa(Poste poste, string? mufaId)
        {
            if (!string.IsNullOrEmpty(mufaId))
            {
                var mufa = await _unitOfWork.Repositorio<Mufa>().ObtenerPorIdAsync(id: mufaId, rastreo: true);

                if (mufa != null)
                {
                    mufa.PosteId = poste.Id;
                }
            }
            else
            {
                mufaId = poste.Mufas.FirstOrDefault()?.Id;

                if (!string.IsNullOrEmpty(mufaId))
                {
                    var mufa = await _unitOfWork.Repositorio<Mufa>().ObtenerPorIdAsync(id: mufaId, rastreo: true);

                    if (mufa != null)
                    {
                        mufa.PosteId = null;
                    }
                }
            }
        }
    }
}
