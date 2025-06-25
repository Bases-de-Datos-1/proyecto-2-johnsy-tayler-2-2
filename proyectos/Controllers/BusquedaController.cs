using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using HotelesCaribe.Models;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> Index(
            string tipoBusqueda = "hospedaje",
            string ubicacion = null,
            string tipoHospedaje = "Hotel",
            string tipoActividad = null,
            string nombreHotel = null,
            string provincia = null,
            string canton = null,
            string servicio = null,
            decimal? precioMinimo = null,
            decimal? precioMaximo = null)
        {
            ubicacion ??= "Puerto Viejo, Limón";
            
            var resultados = new List<VwBusquedaHospedaje>();

            var resultadosHospedaje = new List<VwBusquedaHospedaje>();
            var resultadosActividades = new List<ModelVwBusquedaActividades>();

            if (!tipoBusqueda.Equals("actividades"))
            {
                // Búsqueda de hospedajes
                try
                {
                    if (!string.IsNullOrWhiteSpace(nombreHotel))
                    {
                        var param = new SqlParameter("@nombreHotel", nombreHotel.ToLower());
                        resultados = await _context.VwBusquedaHospedajes
                            .FromSqlRaw("EXEC SP_BuscarHospedajesPorNombre @nombreHotel", param)
                            .ToListAsync();
                    }
                    else
                    {
                        var parametros = new[]
                        {
                            new SqlParameter("@provincia", (object?)provincia ?? DBNull.Value),
                            new SqlParameter("@canton", (object?)canton ?? DBNull.Value),
                            new SqlParameter("@TipoHospedaje", (object?)tipoHospedaje ?? DBNull.Value),
                            new SqlParameter("@servicio", (object?)servicio ?? DBNull.Value),
                            new SqlParameter("@precio_minimo", (object?)precioMinimo ?? DBNull.Value),
                            new SqlParameter("@precio_maximo", (object?)precioMaximo ?? DBNull.Value)
                        };

                        resultados = await _context.VwBusquedaHospedajes
                            .FromSqlRaw("EXEC SP_BuscarHospedajesConFiltros @provincia, @canton, @TipoHospedaje, @servicio, @precio_minimo, @precio_maximo", parametros)
                            .ToListAsync();

                        //resultados.AddRange(resultadosActividades.Cast<dynamic>());
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error en búsqueda: {ex.Message}");
                }
            }
            else
            {
                // Búsqueda de actividades
                try
                {
                    if (!string.IsNullOrWhiteSpace(nombreHotel))
                    {
                        var param = new SqlParameter("@nombreHotel", nombreHotel.ToLower());
                        //resultadosActividades = await _context.VwBusquedaHospedajes
                        //    .FromSqlRaw("EXEC SP_BuscarHospedajesPorNombre @nombreHotel", param)
                        //    .ToListAsync();
                    }
                    else
                    {
                        var parametros = new[]
                        {
                            new SqlParameter("@TipoActividad", (object?)tipoActividad ?? DBNull.Value),
                            new SqlParameter("@precio_maximo", (object?)precioMaximo ?? DBNull.Value),
                            new SqlParameter("@provincia", (object?)provincia ?? DBNull.Value),
                            new SqlParameter("@canton", (object?)canton ?? DBNull.Value)
                        };

                        resultadosActividades = await _context.Database
                           .SqlQueryRaw<ModelVwBusquedaActividades>("EXEC SP_BuscarActividadesConFiltros @TipoActividad, @precio_maximo, @provincia, @canton", parametros)
                           .ToListAsync();

                        //resultados.AddRange(resultadosActividades.Cast<dynamic>());
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error en búsqueda: {ex.Message}");
                }
            }

            ViewBag.Ubicacion = ubicacion;
            ViewBag.TipoHospedaje = tipoHospedaje;
            ViewBag.NombreHotel = nombreHotel;
            ViewBag.TotalResultados = resultados.Count;
            ViewBag.TipoBusqueda = tipoBusqueda;

            return View(resultados);
        }

        // GET: Busqueda/ResultadosBusqueda
        public async Task<IActionResult> ResultadosBusqueda(string ubicacion = null, string tipoHospedaje = "Hotel")
        {
            return RedirectToAction(nameof(Index), new { ubicacion = ubicacion, tipoHospedaje = tipoHospedaje });
        }
    }
}