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

            if (empresaId.HasValue)
            {
                facturas = facturas.Where(f => f.IdReservaNavigation.IdEmpresaHospedaje == empresaId.Value);
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

            var param = new SqlParameter("@idFactura", id);
            var factura = await _context.Facturas
                .FromSqlRaw("EXEC SP_InfoFacturaPorId @idFactura", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create - Mostrar formulario para ingresar número de reserva
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create - Buscar reserva y mostrar detalles para facturar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string IdReserva)
        {
            // Validar que se proporcionó un número de reserva
            if (string.IsNullOrEmpty(IdReserva))
            {
                ModelState.AddModelError("IdReserva", "Debe ingresar un número de reserva");
                return View();
            }

            // Convertir a int si es posible
            if (!int.TryParse(IdReserva, out int reservaId))
            {
                ModelState.AddModelError("IdReserva", "El número de reserva debe ser un valor numérico");
                return View();
            }

            // Buscar la reserva con toda la información relacionada
            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEmpresaHospedajeNavigation)
                .Include(r => r.IdHabitacionNavigation)
                    .ThenInclude(h => h.IdTipoHabitacionNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == reservaId);

            if (reserva == null)
            {
                ModelState.AddModelError("IdReserva", "No se encontró ninguna reserva con ese número");
                return View();
            }

            // Verificar si ya existe una factura para esta reserva
            var facturaExistente = await _context.Facturas
                .FirstOrDefaultAsync(f => f.IdReserva == reservaId);

            if (facturaExistente != null)
            {
                ModelState.AddModelError("IdReserva", "Esta reserva ya tiene una factura generada");
                return View();
            }

            // Redirigir a la vista de confirmación con los datos de la reserva
            return RedirectToAction("Facturar", new { id = reservaId });
        }

        // GET: Facturas/Facturar/5 - Mostrar detalles de la reserva para facturar
        public async Task<IActionResult> Facturar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar la reserva con toda la información
            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEmpresaHospedajeNavigation)
                .Include(r => r.IdHabitacionNavigation)
                    .ThenInclude(h => h.IdTipoHabitacionNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            // Calcular el importe total
            var noches = (reserva.FechaSalida.ToDateTime(TimeOnly.MinValue) - reserva.FechaIngreso).Days;
            var importeTotal = noches * reserva.IdHabitacionNavigation.IdTipoHabitacionNavigation.Precio;

            // Crear el modelo para la vista
            var factura = new Factura
            {
                IdReserva = reserva.IdReserva,
                ImporteTotal = importeTotal,
                Fecha = DateTime.Now
            };

            // Pasar información adicional a la vista
            ViewBag.Reserva = reserva;
            ViewBag.Noches = noches;
            ViewBag.NombreHotel = reserva.IdEmpresaHospedajeNavigation.Nombre;
            ViewBag.NumeroHabitacion = reserva.IdHabitacionNavigation.Numero;
            ViewBag.TipoHabitacion = reserva.IdHabitacionNavigation.IdTipoHabitacionNavigation.Nombre;

            return View(factura);
        }

        // POST: Facturas/Facturar - Procesar la facturación
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Facturar([Bind("IdReserva,ImporteTotal,FormaPago")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                // Establecer la fecha actual
                factura.Fecha = DateTime.Now;

                var parameters = new[]
                {
                    new SqlParameter("@p_idReserva", factura.IdReserva),
                    new SqlParameter("@p_importeTotal", factura.ImporteTotal),
                    new SqlParameter("@p_formaPaGO", factura.FormaPago)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_InsertarFactura @p_idReserva, @p_importeTotal, @p_formaPaGO", parameters);


                // Redirigir a la vista de confirmación con el ID de la factura
                return RedirectToAction("Comprobante", new { id = factura.IdFactura });
            }

            // Si hay errores, recargar la vista con los datos
            var reserva = await _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdEmpresaHospedajeNavigation)
                .Include(r => r.IdHabitacionNavigation)
                    .ThenInclude(h => h.IdTipoHabitacionNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == factura.IdReserva);

            if (reserva != null)
            {
                var noches = (reserva.FechaSalida.ToDateTime(TimeOnly.MinValue) - reserva.FechaIngreso).Days;
                ViewBag.Reserva = reserva;
                ViewBag.Noches = noches;
                ViewBag.NombreHotel = reserva.IdEmpresaHospedajeNavigation.Nombre;
                ViewBag.NumeroHabitacion = reserva.IdHabitacionNavigation.Numero;
                ViewBag.TipoHabitacion = reserva.IdHabitacionNavigation.IdTipoHabitacionNavigation.Nombre;
            }

            return View(factura);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@idFactura", id);
            var factura = await _context.Facturas
                .FromSqlRaw("EXEC SP_InfoFacturaPorId @idFactura", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (factura == null)
            {
                return NotFound();
            }

            ViewData["IdReserva"] = new SelectList(_context.Reservas, "IdReserva", "IdReserva", factura.IdReserva);
            return View(factura);
        }

        /*
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
        */

        // POST: Facturas/Edit/5
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
                    var parameters = new[]
                    {
                        new SqlParameter("@idFactura", factura.IdFactura),
                        new SqlParameter("@idReserva", factura.IdReserva),
                        new SqlParameter("@importeTotal", factura.ImporteTotal),
                        new SqlParameter("@formaPaGO", factura.FormaPago)
                    };

                    await _context.Database.ExecuteSqlRawAsync("EXEC SP_ActualizarFactura @idFactura, @idReserva, @importeTotal, @formaPaGO", parameters);

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

            var param = new SqlParameter("@idFactura", id);
            var factura = await _context.Facturas
                .FromSqlRaw("EXEC SP_InfoFacturaPorId @idFactura", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();

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
            var param = new SqlParameter("@idFactura", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_EliminarFactura @idFactura", param);
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.IdFactura == id);
        }
    }
}