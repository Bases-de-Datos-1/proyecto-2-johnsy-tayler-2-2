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
    public class FacturasController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public FacturasController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index(int? empresaId)
        {
            IQueryable<Factura> facturas = _context.Facturas
                .Include(f => f.IdReservaNavigation)
                    .ThenInclude(r => r.IdClienteNavigation)
                .Include(f => f.IdReservaNavigation)
                    .ThenInclude(r => r.IdEmpresaHospedajeNavigation)
                .Include(f => f.IdReservaNavigation)
                    .ThenInclude(r => r.IdHabitacionNavigation)
                        .ThenInclude(h => h.IdTipoHabitacionNavigation);

            // Si se proporciona un ID de empresa, filtrar por esa empresa
            if (empresaId.HasValue)
            {
                facturas = facturas.Where(f => f.IdReservaNavigation.IdEmpresaHospedaje == empresaId.Value);

                // Obtener información de la empresa para mostrar en la vista
                var empresa = await _context.EmpresaHospedajes
                    .Where(e => e.IdEmpresaHospedaje == empresaId.Value)
                    .FirstOrDefaultAsync();

                ViewBag.EmpresaId = empresaId.Value;
                ViewBag.EmpresaSeleccionada = empresa?.Nombre ?? "Empresa no encontrada";
            }
            else
            {
                ViewBag.EmpresaSeleccionada = "Todas las empresas";
            }

            // Ordenar por fecha descendente (más recientes primero)
            facturas = facturas.OrderByDescending(f => f.Fecha);

            return View(await facturas.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.IdReservaNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,IdReserva,Fecha,ImporteTotal,FormaPago")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", factura.IdReserva);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", factura.IdReserva);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,IdReserva,Fecha,ImporteTotal,FormaPago")] Factura factura)
        {
            if (id != factura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.IdFactura))
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
            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", factura.IdReserva);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.IdReservaNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.IdFactura == id);
        }
    }
}
