using Microsoft.AspNetCore.Mvc;

namespace HotelesCaribe.Controllers
{
    public class AgregarServiciosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // GET: Clientes/Edit/5
        public async Task<IActionResult> SeleccionarTipoEmpresa()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SeleccionarTipoEmpresa(string tipoEmpresa)
        {
            if (tipoEmpresa == "Alojamiento")
            {
                return RedirectToAction("Create", "EmpresaHospedajes");
            }
            else if (tipoEmpresa == "Recreacion")
            {
                return RedirectToAction("Create", "EmpresaRecreacions");
            }
            return View();
        }
    }
}
