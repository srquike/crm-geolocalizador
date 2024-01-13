using Microsoft.AspNetCore.Mvc;

namespace LevantamientoDeRed.Controllers.MVC
{
    public class GeneralController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
