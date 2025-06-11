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
    public class EmpresaRecreacionsController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public EmpresaRecreacionsController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: EmpresaRecreacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpresaRecreacions.ToListAsync());
        }

        // GET: EmpresaRecreacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaRecreacion = await _context.EmpresaRecreacions
                .FirstOrDefaultAsync(m => m.IdEmpresaRecreacion == id);
            if (empresaRecreacion == null)
            {
                return NotFound();
            }

            return View(empresaRecreacion);
        }

        // GET: EmpresaRecreacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpresaRecreacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpresaRecreacion,Nombre,CedulaJuridica,Correo,Telefono,Encargado,Provincia,Canton,Distrito,Senas,Latitud,Longitud")] EmpresaRecreacion empresaRecreacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresaRecreacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresaRecreacion);
        }

        // GET: EmpresaRecreacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaRecreacion = await _context.EmpresaRecreacions.FindAsync(id);
            if (empresaRecreacion == null)
            {
                return NotFound();
            }
            return View(empresaRecreacion);
        }

        // POST: EmpresaRecreacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpresaRecreacion,Nombre,CedulaJuridica,Correo,Telefono,Encargado,Provincia,Canton,Distrito,Senas,Latitud,Longitud")] EmpresaRecreacion empresaRecreacion)
        {
            if (id != empresaRecreacion.IdEmpresaRecreacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresaRecreacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaRecreacionExists(empresaRecreacion.IdEmpresaRecreacion))
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
            return View(empresaRecreacion);
        }

        // GET: EmpresaRecreacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaRecreacion = await _context.EmpresaRecreacions
                .FirstOrDefaultAsync(m => m.IdEmpresaRecreacion == id);
            if (empresaRecreacion == null)
            {
                return NotFound();
            }

            return View(empresaRecreacion);
        }

        // POST: EmpresaRecreacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresaRecreacion = await _context.EmpresaRecreacions.FindAsync(id);
            if (empresaRecreacion != null)
            {
                _context.EmpresaRecreacions.Remove(empresaRecreacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaRecreacionExists(int id)
        {
            return _context.EmpresaRecreacions.Any(e => e.IdEmpresaRecreacion == id);
        }
    }
}
