using HotelesCaribe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

            if (empresaId.HasValue)
            {
                habitaciones = habitaciones.Where(h => h.IdEmpresaHospedaje == empresaId.Value);
                ViewBag.EmpresaId = empresaId.Value;
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
        public IActionResult Create(int? empresaId)
        {
            if (empresaId.HasValue)
            {
                ViewBag.EmpresaIdPreseleccionada = empresaId.Value;
                var empresa = _context.EmpresaHospedajes.Find(empresaId.Value);
                ViewBag.EmpresaSeleccionada = empresa?.Nombre;
            }

            var idEmpresaHospedaje = new SqlParameter("@idEmpresaHospedaje", empresaId);
            var info = _context.TipoHabitacions
                .FromSqlRaw("EXEC SP_TiposHabitacionPorEmpresa @idEmpresaHospedaje", idEmpresaHospedaje)
                .AsEnumerable();
            ViewData["IdEmpresaHospedaje"] = new SelectList(_context.EmpresaHospedajes, "IdEmpresaHospedaje", "Nombre");
            ViewData["IdTipoHabitacion"] = new SelectList(info, "IdTipo", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHabitacion,IdEmpresaHospedaje,Numero,IdTipoHabitacion")] Habitacion habitacion, int? empresaId)
        {
            if (empresaId.HasValue && habitacion.IdEmpresaHospedaje == 0)
            {
                habitacion.IdEmpresaHospedaje = empresaId.Value;
            }

            if (ModelState.IsValid)
            {
                var parameters = new[]
                    {
                        new SqlParameter("@p_idEmpresaHospedaje", habitacion.IdEmpresaHospedaje),
                        new SqlParameter("@p_numero", habitacion.Numero),
                        new SqlParameter("@p_idTipoHabitacion", habitacion.IdTipoHabitacion),
                    };
                
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_InsertarHabitacion @p_idEmpresaHospedaje, @p_numero, @p_idTipoHabitacion",
                    parameters);

                return RedirectToAction(nameof(Index), new { empresaId = habitacion.IdEmpresaHospedaje });
            }
            else
            {
                foreach (var campo in ModelState)
                {
                    foreach (var error in campo.Value.Errors)
                    {
                        ModelState.AddModelError("", $"Campo: {campo.Key}, Error: {error.ErrorMessage}");
                    }
                }
            }

            ViewBag.EmpresaIdPreseleccionada = habitacion.IdEmpresaHospedaje;
            var empresa = _context.EmpresaHospedajes.Find(habitacion.IdEmpresaHospedaje);
            ViewBag.EmpresaSeleccionada = empresa?.Nombre;

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
