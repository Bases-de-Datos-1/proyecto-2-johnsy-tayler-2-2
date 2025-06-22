using HotelesCaribe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Text.Json;

namespace HotelesCaribe.Controllers
{
    public class HospedajeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly GestionHoteleraContext _context;

        public HospedajeController(GestionHoteleraContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Detalles(int id, BusquedaFiltros filtros)
        {
            var hotel = await _context.EmpresaHospedajes
                .FirstOrDefaultAsync(h => h.IdEmpresaHospedaje == id);

            if (hotel == null)
                return NotFound();

            var habitaciones = await _context.Habitacions
                .Include(h => h.IdTipoHabitacionNavigation)
                .Where(h => h.IdEmpresaHospedaje == id)
                .ToListAsync();

            var fotosHotel = await _context.VwFotosEmpresaHospedajes
                .Where(f => f.IdEmpresaHospedaje == id)
                .ToListAsync();

            // sp para filtros de habitaciones
            /*
             * 
             *     @fecha_inicio DATE,
                    @fecha_fin DATE,
                    @idEmpresaHospedaje INT = NULL,
                    @idTipoHabitacion INT = NULL,
                    @precio_maximo DECIMAL(10,2) = NULL,
                    @comodidad VARCHAR(50) = NULL
             * 
             */
            var parameters = new[]
            {
                new SqlParameter("@fecha_inicio", filtros.FechaEntrada ?? (object)DBNull.Value),
                new SqlParameter("@fecha_fin", filtros.FechaSalida ?? (object)DBNull.Value),
                new SqlParameter("@idEmpresaHospedaje", id),
                new SqlParameter("@idTipoHabitacion", DBNull.Value),
                new SqlParameter("@precio_maximo", filtros.PrecioMaximo ?? (object)DBNull.Value),
                new SqlParameter("@comodidad", DBNull.Value),
            };
            habitaciones = _context.Habitacions
                .FromSqlRaw("EXEC SP_BuscarHabitacionesDisponiblesConFiltros @fecha_inicio, @fecha_fin, @idEmpresaHospedaje, @idTipoHabitacion, @precio_maximo, @comodidad", parameters)
                .ToList();

            var viewModel = new HospedajeDetallesModel
            {
                Hotel = hotel,
                Habitaciones = habitaciones,
                FotosHotel = fotosHotel,
                Filtros = filtros
            };

            return View(viewModel);
        }

        // GET: Hospedaje/Reservar/5
        public async Task<IActionResult> Reservar(int id, DateTime? fechaEntrada, DateTime? fechaSalida, int numeroPersonas = 1)
        {
            var habitacion = await _context.Habitacions
                .Include(h => h.IdTipoHabitacionNavigation)
                .Include(h => h.IdEmpresaHospedajeNavigation)
                .FirstOrDefaultAsync(h => h.IdHabitacion == id);

            if (habitacion == null)
                return NotFound();

            var viewModel = new ReservaModel
            {
                IdHabitacion = id,
                Identificacion = User.FindFirst("ClienteIdentificacion")?.Value,
                Hotel = habitacion.IdEmpresaHospedajeNavigation,
                TipoHabitacion = habitacion.IdTipoHabitacionNavigation,
                FechaEntrada = fechaEntrada ?? DateTime.Today.AddDays(1),
                FechaSalida = fechaSalida ?? DateTime.Today.AddDays(2),
                NumeroPersonas = numeroPersonas
            };

            return View(viewModel);
        }
        // POST
        [HttpPost]
        public async Task<IActionResult> ProcesarReserva(ReservaModel modelo)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear(); // CAMBIAR DESPUÉS
            }

            if (!ModelState.IsValid)
            {

                var habitacion = await _context.Habitacions
                    .Include(h => h.IdTipoHabitacionNavigation)
                    .Include(h => h.IdEmpresaHospedajeNavigation)
                    .FirstOrDefaultAsync(h => h.IdHabitacion == modelo.IdHabitacion);

                modelo.Hotel = habitacion?.IdEmpresaHospedajeNavigation;
                modelo.TipoHabitacion = habitacion?.IdTipoHabitacionNavigation;

                return View("Reservar", modelo);
            }

            try
            {
                var habitacion = await _context.Habitacions
                    .Include(h => h.IdEmpresaHospedajeNavigation)
                    .FirstOrDefaultAsync(h => h.IdHabitacion == modelo.IdHabitacion);

                if (habitacion == null)
                {
                    ModelState.AddModelError("", "Habitación no encontrada");
                    return View("Reservar", modelo);
                }

                var parametros = new[]
                {
                    new SqlParameter("@p_idCliente", SqlDbType.Int) { Value = int.Parse(User.FindFirst("ClienteId")?.Value ?? "0")  },
                    new SqlParameter("@p_idEmpresaHospedaje", SqlDbType.Int) { Value = habitacion.IdEmpresaHospedaje },
                    new SqlParameter("@p_idHabitacion", SqlDbType.Int) { Value = modelo.IdHabitacion },
                    new SqlParameter("@p_fechaIngreso", SqlDbType.DateTime) { Value = modelo.FechaEntrada },
                    new SqlParameter("@p_cantidadPersonAS", SqlDbType.Int) { Value = modelo.NumeroPersonas },
                    new SqlParameter("@p_tieneVehiculo", SqlDbType.Bit) { Value = modelo.TieneVehiculo },
                    new SqlParameter("@p_fechASalida", SqlDbType.DateTime) { Value = modelo.FechaSalida },
                    new SqlParameter("@p_horASalida", SqlDbType.Time) { Value = modelo.HoraSalida },
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_InsertarReserva @p_idCliente, @p_idEmpresaHospedaje, @p_idHabitacion, @p_fechaIngreso, @p_cantidadPersonAS, @p_tieneVehiculo, @p_fechASalida, @p_horASalida",
                    parametros
                );

                var reservaReciente = await _context.Reservas
                    .OrderByDescending(r => r.IdReserva)
                    .FirstOrDefaultAsync(r =>
                        r.IdHabitacion == modelo.IdHabitacion &&
                        r.FechaIngreso == modelo.FechaEntrada &&
                        r.FechaSalida == DateOnly.FromDateTime(modelo.FechaSalida)
                    );

                if (reservaReciente != null)
                {
                    return RedirectToAction("ComprobanteReserva", new { id = reservaReciente.IdReserva });
                }
                else
                {
                    var ultimaReserva = await _context.Reservas
                        .OrderByDescending(r => r.IdReserva)
                        .FirstOrDefaultAsync();

                    if (ultimaReserva != null)
                    {
                        return RedirectToAction("ComprobanteReserva", new { id = ultimaReserva.IdReserva });
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo obtener la reserva recién creada.");
                        return View("Reservar", modelo);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al procesar la reserva: " + ex.Message);

                var habitacion = await _context.Habitacions
                    .Include(h => h.IdTipoHabitacionNavigation)
                    .Include(h => h.IdEmpresaHospedajeNavigation)
                    .FirstOrDefaultAsync(h => h.IdHabitacion == modelo.IdHabitacion);

                modelo.Hotel = habitacion?.IdEmpresaHospedajeNavigation;
                modelo.TipoHabitacion = habitacion?.IdTipoHabitacionNavigation;

                return View("Reservar", modelo);
            }
        }

        // GET: ComprobanteReserva
        public async Task<IActionResult> ComprobanteReserva(int id)
        {
            try
            {     
                var info = await _context.VwDetalleReservas
                    .Where(r => r.IdReserva == id)
                    .FirstOrDefaultAsync();

                if (info != null)
                {
                    ViewBag.NumeroReserva = info.IdReserva.ToString("000000");
                    ViewBag.NombreHotel = info.Hotel ?? "Hotel no especificado";
                    ViewBag.IdentificacionCliente = info.Cliente ?? "No especificado";
                    ViewBag.FechaEntrada = info.FechaIngreso.ToString("dd/MM/yyyy");
                    ViewBag.FechaSalida = info.FechAsalida.ToString("dd/MM/yyyy");
                    ViewBag.HoraEntrada = "12:00:00";
                    ViewBag.HoraSalida = info.HorAsalida.ToString(@"hh\:mm\:ss");
                    ViewBag.NumeroHabitaciones = "1";
                    ViewBag.NumeroPersonas = info.CantidadPersonAs.ToString() ?? "1";
                    ViewBag.TieneVehiculo = info.Vehiculo == "Sí" || info.Vehiculo == "True" ? "Sí" : "No";
                    ViewBag.DescripcionHabitacion = $"{info.TipoHabitacion} - Habitación {info.Habitacion}";
                    ViewBag.TotalPagar = info.TotalEstimado?.ToString("₡#,##0.00") ?? "₡0.00";

                    return View();
                }
                else
                {
                    ViewBag.NumeroReserva = id.ToString("000000");
                    ViewBag.NombreHotel = "Reserva no encontrada";
                    ViewBag.IdentificacionCliente = $"ID buscado: {id}";
                    ViewBag.FechaEntrada = DateTime.Now.ToString("dd/MM/yyyy");
                    ViewBag.FechaSalida = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                    ViewBag.HoraEntrada = "12:00:00";
                    ViewBag.HoraSalida = "12:00:00";
                    ViewBag.NumeroHabitaciones = "1";
                    ViewBag.NumeroPersonas = "1";
                    ViewBag.TieneVehiculo = "No";
                    ViewBag.DescripcionHabitacion = "Información no disponible";
                    ViewBag.TotalPagar = "₡0.00";

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.NumeroReserva = id.ToString("000000");
                ViewBag.NombreHotel = "Error al cargar datos";
                ViewBag.IdentificacionCliente = ex.Message;
                ViewBag.FechaEntrada = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.FechaSalida = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                ViewBag.HoraEntrada = "12:00:00";
                ViewBag.HoraSalida = "12:00:00";
                ViewBag.NumeroHabitaciones = "1";
                ViewBag.NumeroPersonas = "1";
                ViewBag.TieneVehiculo = "No";
                ViewBag.DescripcionHabitacion = "Error en la consulta";
                ViewBag.TotalPagar = "₡0.00";

                return View();
            }
        }
    }
}