using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelesCaribe.Models;

namespace HotelesCaribe.Controllers
{
    public class BusquedaController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public BusquedaController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: Busqueda
        public async Task<IActionResult> Index(string ubicacion = null, string tipoHospedaje = "Hotel")
        {
            ubicacion = string.IsNullOrEmpty(ubicacion) ? "Puerto Viejo, Limón" : ubicacion;

            var resultados = await _context.VwBusquedaHospedajes
                .Where(h => string.IsNullOrEmpty(ubicacion) ||
                       h.Provincia.Contains(ubicacion) ||
                       h.Canton.Contains(ubicacion) ||
                       h.Distrito.Contains(ubicacion))
                .Where(h => string.IsNullOrEmpty(tipoHospedaje) ||
                       h.TipoHospedaje == tipoHospedaje)
                .Take(9)
                .ToListAsync();

            ViewBag.Ubicacion = ubicacion;
            ViewBag.TipoHospedaje = tipoHospedaje;

            return View(resultados);
        }

        // GET: Busqueda/ResultadosBusqueda
        public async Task<IActionResult> ResultadosBusqueda(string ubicacion = null, string tipoHospedaje = "Hotel")
        {
            return RedirectToAction(nameof(Index), new { ubicacion = ubicacion, tipoHospedaje = tipoHospedaje });
        }
    }
}