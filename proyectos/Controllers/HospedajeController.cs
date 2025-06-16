using HotelesCaribe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Detalles(int id)
        {
            var hotel = await _context.EmpresaHospedajes
                .FirstOrDefaultAsync(h => h.IdEmpresaHospedaje == id);

            if (hotel == null)
                return NotFound();

            var habitaciones = await _context.Habitacions
                .Include(h => h.IdTipoHabitacionNavigation)
                .Where(h => h.IdEmpresaHospedaje == id)
                .ToListAsync();

            var viewModel = new HospedajeDetallesModel
            {
                Hotel = hotel,
                Habitaciones = habitaciones,
                Filtros = new BusquedaFiltros()
            };

            return View(viewModel);
        }

        // Agregar estas acciones a tu HospedajeController

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
                Hotel = habitacion.IdEmpresaHospedajeNavigation,
                TipoHabitacion = habitacion.IdTipoHabitacionNavigation,
                FechaEntrada = fechaEntrada ?? DateTime.Today.AddDays(1),
                FechaSalida = fechaSalida ?? DateTime.Today.AddDays(2),
                NumeroPersonas = numeroPersonas
            };

            return View(viewModel);
        }

        // POST: Hospedaje/ConfirmarReserva
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarReserva(ReservaModel model)
        {
            if (ModelState.IsValid)
            {
                
            }

            var habitacionData = await _context.Habitacions
                .Include(h => h.IdTipoHabitacionNavigation)
                .Include(h => h.IdEmpresaHospedajeNavigation)
                .FirstOrDefaultAsync(h => h.IdHabitacion == model.IdHabitacion);

            model.Hotel = habitacionData.IdEmpresaHospedajeNavigation;
            model.TipoHabitacion = habitacionData.IdTipoHabitacionNavigation;

            return View("Reservar", model);
        }
    }
}