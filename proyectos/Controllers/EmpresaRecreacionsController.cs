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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpresaRecreacion,Nombre,CedulaJuridica,Correo,Telefono,Encargado,Provincia,Canton,Distrito,Senas,Latitud,Longitud")] EmpresaRecreacion empresaRecreacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresaRecreacion);
                await _context.SaveChangesAsync();

                return RedirectToAction("AgregarTiposServicios", new { id = empresaRecreacion?.IdEmpresaRecreacion });
            }
            return View(empresaRecreacion);
        }

        /* AGREGAR TIPOS DE SERVICIOS */

        // GET: EmpresaRecreacions/AgregarTiposServicios/5
        public async Task<IActionResult> AgregarTiposServicios(int? id)
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

            var modelo = new ModeloRecreativaActividades
            {
                IdEmpresa = empresaRecreacion.IdEmpresaRecreacion
            };

            return View(modelo);
        }

        // POST: EmpresaRecreacions/AgregarTiposServicios/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarTiposServicios(int id, ModeloRecreativaActividades modelo, List<string> ServiciosSeleccionados)
        {
            if (id != modelo.IdEmpresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid && ServiciosSeleccionados != null && ServiciosSeleccionados.Any())
            {
                var empresa = await _context.EmpresaRecreacions.FindAsync(id);
                if (empresa != null)
                {
                    // Aquí guardarías los tipos de servicios seleccionados en tu base de datos
                    // Esto dependerá de tu estructura de base de datos

                    // Ejemplo si tienes una tabla TiposServiciosEmpresa:
                    /*
                    // Eliminar tipos de servicios existentes
                    var serviciosExistentes = _context.TiposServiciosEmpresas
                        .Where(s => s.IdEmpresaRecreacion == id);
                    _context.TiposServiciosEmpresas.RemoveRange(serviciosExistentes);

                    // Agregar nuevos tipos de servicios
                    foreach (var servicio in ServiciosSeleccionados)
                    {
                        var nuevoServicio = new TiposServiciosEmpresa
                        {
                            IdEmpresaRecreacion = id,
                            TipoServicio = servicio,
                            FechaCreacion = DateTime.Now
                        };
                        _context.TiposServiciosEmpresas.Add(nuevoServicio);
                    }
                    */

                    await _context.SaveChangesAsync();

                    return RedirectToAction("AgregarActividades", new { id = id });
                }
            }

            // Si no se seleccionaron servicios, agregar error
            if (ServiciosSeleccionados == null || !ServiciosSeleccionados.Any())
            {
                ModelState.AddModelError("", "Debe seleccionar al menos un tipo de servicio.");
            }

            return View(modelo);
        }

        /* FIN AGREGAR TIPOS DE SERVICIOS */

        /* AGREGAR ACTIVIDADES */

        // GET: EmpresaRecreacions/AgregarActividades/5
        public async Task<IActionResult> AgregarActividades(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaRecreacions = await _context.EmpresaRecreacions.FindAsync(id);
            if (empresaRecreacions == null)
            {
                return NotFound();
            }

            var modelo = new ModeloRecreativaActividades
            {
                IdEmpresa = empresaRecreacions.IdEmpresaRecreacion
            };

            return View(modelo);
        }

        // POST: EmpresaRecreacions/AgregarActividades/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarActividades(int id, ModeloRecreativaActividades modelo)
        {
            if (id != modelo.IdEmpresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var empresa = await _context.EmpresaRecreacions.FindAsync(id);
                if (empresa != null)
                {
                    // Aquí guardarías las actividades en tu base de datos
                    // Esto dependerá de tu estructura de base de datos

                    // Ejemplo si tienes una tabla ActividadesEmpresa:
                    /*
                    // Eliminar actividades existentes
                    var actividadesExistentes = _context.ActividadesEmpresas
                        .Where(a => a.IdEmpresaRecreacions == id);
                    _context.ActividadesEmpresas.RemoveRange(actividadesExistentes);

                    // Agregar nuevas actividades
                    foreach (var actividad in modelo.Actividades)
                    {
                        var nuevaActividad = new ActividadesEmpresa
                        {
                            IdEmpresaRecreacions = id,
                            Nombre = actividad.Nombre,
                            Descripcion = actividad.Descripcion,
                            Precio = actividad.Precio,
                            FechaCreacion = DateTime.Now
                        };
                        _context.ActividadesEmpresas.Add(nuevaActividad);
                    }
                    */

                    await _context.SaveChangesAsync();

                    // IR A PANEL DE CONTROL DE RECREACIÓN
                    return RedirectToAction("PanelControlRecreativa", new {id = modelo.IdEmpresa});
                }
            }

            return View(modelo);
        }

        /* FIN AGREGAR ACTIVIDADES */


        // GET: EmpresaRecreacions/PanelControlRecreativa/5
        public async Task<IActionResult> PanelControlRecreativa(int? id)
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