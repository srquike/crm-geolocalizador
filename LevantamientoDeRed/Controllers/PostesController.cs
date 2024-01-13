using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers
{
    public class PostesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger;

        public PostesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsuariosController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: PostesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PostesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostesController/Create
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

        // GET: PostesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: PostesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        //Vistas de postes

        [HttpGet]
        public ActionResult Crear()
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
                    ViewData["error_obtener_poste"] = "No fue posible obtener los datos del poste";
                    return View();
                }

                var poste = await _unitOfWork.Repositorio<Poste>().ObtenerPorIdAsync(id, rastreo: false);

                if (poste == null)
                {
                    ViewData["error_obtener_poste"] = "No fue posible obtener los datos del poste";
                    return View();
                }

                var result = _mapper.Map<PosteDto>(poste);

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos del poste: {Message}", ex.Message);
                ViewData["error_obtener_poste"] = "No fue posible obtener los datos del poste";
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
                    ViewData["error_obtener_poste"] = "No fue posible obtener los datos del poste";
                    return View();
                }

                var poste = await _unitOfWork.Repositorio<Poste>().ObtenerPorIdAsync(id, rastreo: false);

                if (poste == null)
                {
                    ViewData["error_obtener_poste"] = "No fue posible obtener los datos del poste";
                    return View();
                }

                _unitOfWork.Repositorio<Poste>().Eliminar(poste);

                if (await _unitOfWork.SaveChangesAsync())
                    return View(nameof(Index));

                ViewData["error_obtener_poste"] = "No fue posible eliminar los datos del poste";
                return View(_mapper.Map<PosteDto>(poste));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible eliminar o obtener los datos del poste: {Message}", ex.Message);
                ViewData["error_obtener_poste"] = "No fue posible eliminar o obtener los datos del poste";
                return View();
            }
        }
    }
}
