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
    public class EmpresaHospedajesController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public EmpresaHospedajesController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: EmpresaHospedajes
        public async Task<IActionResult> Index()
        {
            var gestionHoteleraContext = _context.EmpresaHospedajes.Include(e => e.IdTipoHospedajeNavigation);
            return View(await gestionHoteleraContext.ToListAsync());
        }

        // GET: EmpresaHospedajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaHospedaje = await _context.EmpresaHospedajes
                .Include(e => e.IdTipoHospedajeNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpresaHospedaje == id);
            if (empresaHospedaje == null)
            {
                return NotFound();
            }

            return View(empresaHospedaje);
        }

        // GET: EmpresaHospedajes/Create
        public IActionResult Create()
        {
            ViewData["IdTipoHospedaje"] = new SelectList(_context.TipoHospedajes, "IdTipoHospedaje", "IdTipoHospedaje");
            return View();
        }

        // POST: EmpresaHospedajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpresaHospedaje,Nombre,CedulaJuridica,IdTipoHospedaje,Provincia,Canton,Distrito,Barrio,Senas,Latitud,Longitud,Correo")] EmpresaHospedaje empresaHospedaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresaHospedaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoHospedaje"] = new SelectList(_context.TipoHospedajes, "IdTipoHospedaje", "IdTipoHospedaje", empresaHospedaje.IdTipoHospedaje);
            return View(empresaHospedaje);
        }

        // GET: EmpresaHospedajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaHospedaje = await _context.EmpresaHospedajes.FindAsync(id);
            if (empresaHospedaje == null)
            {
                return NotFound();
            }
            ViewData["IdTipoHospedaje"] = new SelectList(_context.TipoHospedajes, "IdTipoHospedaje", "IdTipoHospedaje", empresaHospedaje.IdTipoHospedaje);
            return View(empresaHospedaje);
        }

        // POST: EmpresaHospedajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpresaHospedaje,Nombre,CedulaJuridica,IdTipoHospedaje,Provincia,Canton,Distrito,Barrio,Senas,Latitud,Longitud,Correo")] EmpresaHospedaje empresaHospedaje)
        {
            if (id != empresaHospedaje.IdEmpresaHospedaje)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresaHospedaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaHospedajeExists(empresaHospedaje.IdEmpresaHospedaje))
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
            ViewData["IdTipoHospedaje"] = new SelectList(_context.TipoHospedajes, "IdTipoHospedaje", "IdTipoHospedaje", empresaHospedaje.IdTipoHospedaje);
            return View(empresaHospedaje);
        }

        // GET: EmpresaHospedajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaHospedaje = await _context.EmpresaHospedajes
                .Include(e => e.IdTipoHospedajeNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpresaHospedaje == id);
            if (empresaHospedaje == null)
            {
                return NotFound();
            }

            return View(empresaHospedaje);
        }

        // POST: EmpresaHospedajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresaHospedaje = await _context.EmpresaHospedajes.FindAsync(id);
            if (empresaHospedaje != null)
            {
                _context.EmpresaHospedajes.Remove(empresaHospedaje);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaHospedajeExists(int id)
        {
            return _context.EmpresaHospedajes.Any(e => e.IdEmpresaHospedaje == id);
        }
    }
}
