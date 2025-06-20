using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelesCaribe.Models;
using Microsoft.Data.SqlClient;

namespace HotelesCaribe.Controllers
{
    public class TipoHabitacionsController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public TipoHabitacionsController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: TipoHabitacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoHabitacions.ToListAsync());
        }

        // GET: TipoHabitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoHabitacion = await _context.TipoHabitacions
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoHabitacion == null)
            {
                return NotFound();
            }

            return View(tipoHabitacion);
        }

        // GET: TipoHabitacions/Create
        public IActionResult Create(int? empresaId)
        {
            ViewBag.EmpresaId = empresaId;
            return View();
        }

        // POST: TipoHabitacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,Nombre,Descripcion,TipoCama,Precio")] TipoHabitacion tipoHabitacion, int? empresaId)
        {
            if (ModelState.IsValid)
            {
                var parameters = new[]
                    {
                        new SqlParameter("@idEmpresaHospedaje", empresaId),
                        new SqlParameter("@p_nombre", tipoHabitacion.Nombre),
                        new SqlParameter("@p_descripcion", tipoHabitacion.Descripcion),
                        new SqlParameter("@p_tipoCama", tipoHabitacion.TipoCama),
                        new SqlParameter("@p_precio", tipoHabitacion.Precio),
                    };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_InsertarTipoHabitacion @idEmpresaHospedaje, @p_nombre, @p_descripcion, @p_tipoCama, @p_precio",
                    parameters);
                return RedirectToAction(nameof(Index), new { empresaId = empresaId });
            }
            return View(tipoHabitacion);
        }

        // GET: TipoHabitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoHabitacion = await _context.TipoHabitacions.FindAsync(id);
            if (tipoHabitacion == null)
            {
                return NotFound();
            }
            return View(tipoHabitacion);
        }

        // POST: TipoHabitacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,Nombre,Descripcion,TipoCama,Precio")] TipoHabitacion tipoHabitacion)
        {
            if (id != tipoHabitacion.IdTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoHabitacionExists(tipoHabitacion.IdTipo))
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
            return View(tipoHabitacion);
        }

        // GET: TipoHabitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoHabitacion = await _context.TipoHabitacions
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tipoHabitacion == null)
            {
                return NotFound();
            }

            return View(tipoHabitacion);
        }

        // POST: TipoHabitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoHabitacion = await _context.TipoHabitacions.FindAsync(id);
            if (tipoHabitacion != null)
            {
                _context.TipoHabitacions.Remove(tipoHabitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoHabitacionExists(int id)
        {
            return _context.TipoHabitacions.Any(e => e.IdTipo == id);
        }
    }
}
