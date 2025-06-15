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
    public class HabitacionsController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public HabitacionsController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: Habitacions
        public async Task<IActionResult> Index(int? empresaId)
        {
            IQueryable<Habitacion> habitaciones = _context.Habitacions
                .Include(h => h.IdEmpresaHospedajeNavigation)
                .Include(h => h.IdTipoHabitacionNavigation);


            // Si viene empresaId del panel de control, filtrar por esa empresa
            if (empresaId.HasValue)
            {
                habitaciones = habitaciones.Where(h => h.IdEmpresaHospedaje == empresaId.Value);
                // Opcional: pasar el nombre de la empresa a la vista
                ViewBag.EmpresaSeleccionada = await _context.EmpresaHospedajes
                    .Where(e => e.IdEmpresaHospedaje == empresaId.Value)
                    .Select(e => e.Nombre)
                    .FirstOrDefaultAsync();
            }

            return View(await habitaciones.ToListAsync());
        }

        // GET: Habitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacions
                .Include(h => h.IdEmpresaHospedajeNavigation)
                .Include(h => h.IdTipoHabitacionNavigation)
                .FirstOrDefaultAsync(m => m.IdHabitacion == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // GET: Habitacions/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "Nombre");
            ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitacions, "IdTipo", "Nombre");
            return View();
        }

        // POST: Habitacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHabitacion,IdEmpresaHospedaje,Numero,IdTipoHabitacion")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = habitacion?.IdEmpresaHospedaje });
            }
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "Nombre", habitacion.IdEmpresaHospedaje);
            ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitacions, "IdTipo", "Nombre", habitacion?.IdTipoHabitacion);
            return View(habitacion);
        }

        // GET: Habitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacions.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje", habitacion.IdEmpresaHospedaje);
            ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitacions, "IdTipo", "IdTipo", habitacion.IdTipoHabitacion);
            return View(habitacion);
        }

        // POST: Habitacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHabitacion,IdEmpresaHospedaje,Numero,IdTipoHabitacion")] Habitacion habitacion)
        {
            if (id != habitacion.IdHabitacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionExists(habitacion.IdHabitacion))
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
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje", habitacion.IdEmpresaHospedaje);
            ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitacions, "IdTipo", "IdTipo", habitacion.IdTipoHabitacion);
            return View(habitacion);
        }

        // GET: Habitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacions
                .Include(h => h.IdEmpresaHospedajeNavigation)
                .Include(h => h.IdTipoHabitacionNavigation)
                .FirstOrDefaultAsync(m => m.IdHabitacion == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // POST: Habitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitacion = await _context.Habitacions.FindAsync(id);
            if (habitacion != null)
            {
                _context.Habitacions.Remove(habitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionExists(int id)
        {
            return _context.Habitacions.Any(e => e.IdHabitacion == id);
        }
    }
}
