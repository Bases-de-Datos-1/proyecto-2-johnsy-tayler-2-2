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
    public class ServiciosHospedajeController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public ServiciosHospedajeController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: ServiciosHospedaje
        public async Task<IActionResult> Index()
        {
            var gestionHoteleraContext = _context.ServiciosHospedajes.Include(s => s.IdEmpresaHospedajeNavigation).Include(s => s.IdTipoServicioNavigation);
            return View(await gestionHoteleraContext.ToListAsync());
        }

        // GET: ServiciosHospedaje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviciosHospedaje = await _context.ServiciosHospedajes
                .Include(s => s.IdEmpresaHospedajeNavigation)
                .Include(s => s.IdTipoServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdServicio == id);
            if (serviciosHospedaje == null)
            {
                return NotFound();
            }

            return View(serviciosHospedaje);
        }

        // GET: ServiciosHospedaje/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje");
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicioHospedajes, "IdTipoServicio", "IdTipoServicio");
            return View();
        }

        // POST: ServiciosHospedaje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServicio,IdEmpresaHospedaje,IdTipoServicio")] ServiciosHospedaje serviciosHospedaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviciosHospedaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje", serviciosHospedaje.IdEmpresaHospedaje);
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicioHospedajes, "IdTipoServicio", "IdTipoServicio", serviciosHospedaje.IdTipoServicio);
            return View(serviciosHospedaje);
        }

        // GET: ServiciosHospedaje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviciosHospedaje = await _context.ServiciosHospedajes.FindAsync(id);
            if (serviciosHospedaje == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje", serviciosHospedaje.IdEmpresaHospedaje);
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicioHospedajes, "IdTipoServicio", "IdTipoServicio", serviciosHospedaje.IdTipoServicio);
            return View(serviciosHospedaje);
        }

        // POST: ServiciosHospedaje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServicio,IdEmpresaHospedaje,IdTipoServicio")] ServiciosHospedaje serviciosHospedaje)
        {
            if (id != serviciosHospedaje.IdServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviciosHospedaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiciosHospedajeExists(serviciosHospedaje.IdServicio))
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
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "IdEmpresaHospedaje", serviciosHospedaje.IdEmpresaHospedaje);
            ViewData["IdTipoServicio"] = new SelectList(_context.TipoServicioHospedajes, "IdTipoServicio", "IdTipoServicio", serviciosHospedaje.IdTipoServicio);
            return View(serviciosHospedaje);
        }

        // GET: ServiciosHospedaje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviciosHospedaje = await _context.ServiciosHospedajes
                .Include(s => s.IdEmpresaHospedajeNavigation)
                .Include(s => s.IdTipoServicioNavigation)
                .FirstOrDefaultAsync(m => m.IdServicio == id);
            if (serviciosHospedaje == null)
            {
                return NotFound();
            }

            return View(serviciosHospedaje);
        }

        // POST: ServiciosHospedaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviciosHospedaje = await _context.ServiciosHospedajes.FindAsync(id);
            if (serviciosHospedaje != null)
            {
                _context.ServiciosHospedajes.Remove(serviciosHospedaje);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiciosHospedajeExists(int id)
        {
            return _context.ServiciosHospedajes.Any(e => e.IdServicio == id);
        }
    }
}
