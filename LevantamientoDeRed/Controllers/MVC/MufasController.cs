using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Repositories;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.MVC
{
    public class MufasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuariosController> _logger;

        public MufasController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsuariosController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: MufasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MufasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MufasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MufasController/Create
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

        // GET: MufasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MufasController/Edit/5
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

        // GET: MufasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MufasController/Delete/5
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
                    ViewData["error_obtener"] = "No fue posible obtener los datos de la mufa";
                    return View();
                }

                var mufa = await _unitOfWork.Repositorio<Mufa>().ObtenerPorIdAsync(id);

                if (mufa == null)
                {
                    ViewData["error_obtener"] = "No fue posible obtener los datos de la mufa";
                    return View();
                }

                var result = _mapper.Map<MufaDto>(mufa);

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible obtener los datos de la mufa: {Message}", ex.Message);
                ViewData["error_obtener"] = "No fue posible obtener los datos de la mufa";
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
                    ViewData["error_obtener"] = "No fue posible obtener los datos de la mufa";
                    return View();
                }

                var mufa = await _unitOfWork.Repositorio<Mufa>().ObtenerPorIdAsync(id);

                if (mufa == null)
                {
                    ViewData["error_obtener"] = "No fue posible obtener los datos de la mufa";
                    return View();
                }

                _unitOfWork.Repositorio<Mufa>().Eliminar(mufa);

                if (await _unitOfWork.SaveChangesAsync())
                    return View(nameof(Index));

                ViewData["error_obtener"] = "No fue posible eliminar los datos de la mufa";
                return View(_mapper.Map<PosteDto>(mufa));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible eliminar o obtener los datos de la mufa: {Message}", ex.Message);
                ViewData["error_obtener"] = "No fue posible eliminar o obtener los datos de la mufa";
                return View();
            }
        }
    }
}
