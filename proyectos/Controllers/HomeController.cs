using System.Diagnostics;
using HotelesCaribe.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelesCaribe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string tipoBusqueda = "hospedaje")
        {
            ViewBag.TipoBusqueda = tipoBusqueda;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
