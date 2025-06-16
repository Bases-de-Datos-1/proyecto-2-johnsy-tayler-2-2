using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
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
        public async Task<IActionResult> Index(string ubicacion = null, string tipoHospedaje = "Hotel", string nombreHotel = null)
        {
            ubicacion = string.IsNullOrEmpty(ubicacion) ? "Puerto Viejo, Limón" : ubicacion;
            List<VwBusquedaHospedaje> resultados = new List<VwBusquedaHospedaje>();

            try
            {
                if (!string.IsNullOrWhiteSpace(nombreHotel))
                {

                    var parametro = new SqlParameter("@nombreHotel", nombreHotel);
                    resultados = await _context.VwBusquedaHospedajes
                        .FromSqlRaw("EXEC SP_BuscarHospedajesPorNombre @nombreHotel", parametro)
                        .ToListAsync();
                }
                /*
                else
                {
                    resultados = await _context.VwBusquedaHospedajes
                        .Where(h => string.IsNullOrEmpty(ubicacion) ||
                               h.Provincia.Contains(ubicacion) ||
                               h.Canton.Contains(ubicacion) ||
                               h.Distrito.Contains(ubicacion))
                        .Where(h => string.IsNullOrEmpty(tipoHospedaje) ||
                               h.TipoHospedaje == tipoHospedaje)
                        .Take(9)
                        .ToListAsync();
                }
                */
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en búsqueda: {ex.Message}");
                /*
                resultados = await _context.VwBusquedaHospedajes
                    .Where(h => string.IsNullOrEmpty(ubicacion) ||
                           h.Provincia.Contains(ubicacion) ||
                           h.Canton.Contains(ubicacion) ||
                           h.Distrito.Contains(ubicacion))
                    .Where(h => string.IsNullOrEmpty(tipoHospedaje) ||
                           h.TipoHospedaje == tipoHospedaje)
                    .Take(9)
                    .ToListAsync();
                */
            }

            ViewBag.Ubicacion = ubicacion;
            ViewBag.TipoHospedaje = tipoHospedaje;
            ViewBag.NombreHotel = nombreHotel;
            ViewBag.TotalResultados = resultados.Count;

            return View(resultados);
        }

        // GET: Busqueda/ResultadosBusqueda
        public async Task<IActionResult> ResultadosBusqueda(string ubicacion = null, string tipoHospedaje = "Hotel")
        {
            return RedirectToAction(nameof(Index), new { ubicacion = ubicacion, tipoHospedaje = tipoHospedaje });
        }
    }
}