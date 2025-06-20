using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelesCaribe.Models;

namespace HotelesCaribe.Controllers
{
    public class ReservasController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public ReservasController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index(int? id)
        {
            IQueryable<Reserva> reservas = _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEmpresaHospedajeNavigation)
                .Include(r => r.IdHabitacionNavigation)
                    .ThenInclude(h => h.IdTipoHabitacionNavigation);

            // Si se proporciona un ID de empresa, filtrar por esa empresa
            if (id.HasValue)
            {
                reservas = reservas.Where(r => r.IdEmpresaHospedaje == id.Value);

                // Obtener información de la empresa para mostrar en la vista
                var empresa = await _context.EmpresaHospedajes
                    .Where(e => e.IdEmpresaHospedaje == id.Value)
                    .FirstOrDefaultAsync();

                ViewBag.EmpresaId = id.Value;
                ViewBag.EmpresaSeleccionada = empresa?.Nombre ?? "Empresa no encontrada";
            }
            else
            {
                ViewBag.EmpresaSeleccionada = "Todas las empresas";
            }

            // Ordenar por fecha de ingreso descendente (más recientes primero)
            reservas = reservas.OrderByDescending(r => r.FechaIngreso);

            return View(await reservas.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEmpresaHospedajeNavigation)
                .Include(r => r.IdHabitacionNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje");
            ViewData["IdHabitacion"] = new SelectList(_context.Habitacions, "IdHabitacion", "IdHabitacion");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReserva,IdCliente,IdEmpresaHospedaje,IdHabitacion,FechaIngreso,CantidadPersonas,TieneVehiculo,FechaSalida,HoraSalida,Estado")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", reserva.IdCliente);
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje", reserva.IdEmpresaHospedaje);
            ViewData["IdHabitacion"] = new SelectList(_context.Habitacions, "IdHabitacion", "IdHabitacion", reserva.IdHabitacion);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", reserva.IdCliente);
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje", reserva.IdEmpresaHospedaje);
            ViewData["IdHabitacion"] = new SelectList(_context.Habitacions, "IdHabitacion", "IdHabitacion", reserva.IdHabitacion);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,IdCliente,IdEmpresaHospedaje,IdHabitacion,FechaIngreso,CantidadPersonas,TieneVehiculo,FechaSalida,HoraSalida,Estado")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", reserva.IdCliente);
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje", reserva.IdEmpresaHospedaje);
            ViewData["IdHabitacion"] = new SelectList(_context.Habitacions, "IdHabitacion", "IdHabitacion", reserva.IdHabitacion);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEmpresaHospedajeNavigation)
                .Include(r => r.IdHabitacionNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}
