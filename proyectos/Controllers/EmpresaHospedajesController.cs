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

        /* INICIO SERVICIOS */

        // GET: EmpresaHospedajes/Servicios/5
        public async Task<IActionResult> Servicios(int? id)
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

            var modelo = new ModeloEmpresaServicios
            {
                IdEmpresa = empresaHospedaje.IdEmpresaHospedaje
            };

            return View(modelo);
        }

        // POST: EmpresaHospedajes/Servicios/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Servicios(int id, ModeloEmpresaServicios modelo)
        {
            if (id != modelo.IdEmpresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var empresa = await _context.EmpresaHospedajes.FindAsync(id);
                if (empresa != null)
                {
                    // Aquí necesitas implementar la lógica para guardar los servicios
                    // Esto dependerá de tu estructura de base de datos

                    // Ejemplo si tienes una tabla ServiciosHospedaje:
                    /*
                    var serviciosAActualizar = new List<ServiciosHospedaje>();

                    if (modelo.TienePiscina)
                    {
                        serviciosAActualizar.Add(new ServiciosHospedaje 
                        { 
                            IdEmpresaHospedaje = id, 
                            IdServicio = 1 // ID del servicio piscina 
                        });
                    }

                    if (modelo.TieneWifi)
                    {
                        serviciosAActualizar.Add(new ServiciosHospedaje 
                        { 
                            IdEmpresaHospedaje = id, 
                            IdServicio = 2 // ID del servicio wifi 
                        });
                    }

                    // Continuar con los demás servicios...

                    // Eliminar servicios existentes de esta empresa
                    var serviciosExistentes = _context.ServiciosHospedajes
                        .Where(s => s.IdEmpresaHospedaje == id);
                    _context.ServiciosHospedajes.RemoveRange(serviciosExistentes);

                    // Agregar los nuevos servicios seleccionados
                    _context.ServiciosHospedajes.AddRange(serviciosAActualizar);
                    */

                    await _context.SaveChangesAsync();

                    // Redirigir a la siguiente pantalla o al índice
                    return RedirectToAction("AgregarImagenes" , new { id = id });
                }
            }

            return View(modelo);
        }

        /* FIN SERVICIOS */


        // GET: EmpresaHospedajes/RedesSociales/5
        public async Task<IActionResult> RedesSociales(int? id)
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

            var modelo = new ModeloEmpresaRedes
            {
                IdEmpresa = empresaHospedaje.IdEmpresaHospedaje
            };

            return View(modelo);
        }

        // Agrega este método después del GET RedesSociales en tu controller

        // POST: EmpresaHospedajes/RedesSociales/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RedesSociales(int id, ModeloEmpresaRedes modelo)
        {
            if (id != modelo.IdEmpresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Aquí puedes guardar la información de redes sociales
                // Dependiendo de tu modelo de datos, podrías necesitar crear una tabla Redes
                // o agregar campos a la tabla EmpresaHospedaje

                // Por ejemplo, si tienes una tabla Redes:
                /*
                var empresa = await _context.EmpresaHospedajes.FindAsync(id);
                if (empresa != null)
                {
                    // Eliminar redes existentes
                    var redesExistentes = _context.Redes.Where(r => r.IdEmpresaHospedaje == id);
                    _context.Redes.RemoveRange(redesExistentes);

                    // Agregar nuevas redes
                    var nuevasRedes = new List<Rede>();

                    if (!string.IsNullOrEmpty(modelo.Facebook))
                        nuevasRedes.Add(new Rede { IdEmpresaHospedaje = id, TipoRed = "Facebook", Url = modelo.Facebook });

                    if (!string.IsNullOrEmpty(modelo.Instagram))
                        nuevasRedes.Add(new Rede { IdEmpresaHospedaje = id, TipoRed = "Instagram", Url = modelo.Instagram });

                    if (!string.IsNullOrEmpty(modelo.YouTube))
                        nuevasRedes.Add(new Rede { IdEmpresaHospedaje = id, TipoRed = "YouTube", Url = modelo.YouTube });

                    if (!string.IsNullOrEmpty(modelo.TikTok))
                        nuevasRedes.Add(new Rede { IdEmpresaHospedaje = id, TipoRed = "TikTok", Url = modelo.TikTok });

                    if (!string.IsNullOrEmpty(modelo.Airbnb))
                        nuevasRedes.Add(new Rede { IdEmpresaHospedaje = id, TipoRed = "Airbnb", Url = modelo.Airbnb });

                    if (!string.IsNullOrEmpty(modelo.Threads))
                        nuevasRedes.Add(new Rede { IdEmpresaHospedaje = id, TipoRed = "Threads", Url = modelo.Threads });

                    if (!string.IsNullOrEmpty(modelo.X))
                        nuevasRedes.Add(new Rede { IdEmpresaHospedaje = id, TipoRed = "X", Url = modelo.X });

                    _context.Redes.AddRange(nuevasRedes);
                    await _context.SaveChangesAsync();
                }
                */

                // Redirigir a la página de servicios CON EL ID
                return RedirectToAction("Servicios", new { id = id });
            }

            return View(modelo);
        }

        /* INICIO IMAGENES */

        // GET: EmpresaHospedajes/AgregarImagenes/5
        public async Task<IActionResult> AgregarImagenes(int? id)
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

            var modelo = new ModeloEmpresaImagenes
            {
                IdEmpresa = empresaHospedaje.IdEmpresaHospedaje
            };

            return View(modelo);
        }

        // POST: EmpresaHospedajes/AgregarImagenes/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarImagenes(int id, ModeloEmpresaImagenes modelo)
        {
            if (id != modelo.IdEmpresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var empresa = await _context.EmpresaHospedajes.FindAsync(id);
                if (empresa != null)
                {
                    // Aquí guardar las imágenes en tu base de datos
                    // Ejemplo si tienes una tabla ImagenesEmpresa:
                    /*
                    // Eliminar imágenes existentes
                    var imagenesExistentes = _context.ImagenesEmpresas
                        .Where(img => img.IdEmpresaHospedaje == id);
                    _context.ImagenesEmpresas.RemoveRange(imagenesExistentes);

                    // Agregar nuevas imágenes
                    foreach (var urlImagen in modelo.ImagenesUrls)
                    {
                        var nuevaImagen = new ImagenesEmpresa
                        {
                            IdEmpresaHospedaje = id,
                            UrlImagen = urlImagen,
                            FechaCreacion = DateTime.Now
                        };
                        _context.ImagenesEmpresas.Add(nuevaImagen);
                    }
                    */

                    await _context.SaveChangesAsync();
                    return RedirectToAction("PanelControl", new { id = id }); // O la siguiente pantalla
                }
            }

            return View(modelo);
        }

        /* FIN IMAGENES */



        /* PANEL DE CONTROL */

        // GET: EmpresaHospedajes/PanelControl/5
        public async Task<IActionResult> PanelControl(int? id)
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

        /* FIN PANEL DE CONTROL */


        /* PANEL HABITACIONES */
        // GET: EmpresaHospedajes/PanelHabitaciones/5
        public async Task<IActionResult> PanelHabitaciones(int? id)
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

            // Obtener las habitaciones de esta empresa
            var habitaciones = await _context.Habitacions
                .Include(h => h.IdTipoHabitacionNavigation)
                .Where(h => h.IdEmpresaHospedaje == id)
                .ToListAsync();

            // Pasar datos a la vista
            ViewBag.Habitaciones = habitaciones;
            ViewBag.EmpresaId = id;

            return View(empresaHospedaje);
        }
        /* FIN PANEL HABITACIONES */


        // GET: EmpresaHospedajes/Create
        public IActionResult Create()
        {
            ViewData["IdTipoHospedaje"] = new SelectList(_context.TipoHospedajes, "IdTipoHospedaje", "Nombre");
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

                // Redirigir a la página de redes sociales en lugar de Index
                return RedirectToAction("RedesSociales", new { id = empresaHospedaje?.IdEmpresaHospedaje });
            }
            ViewData["IdTipoHospedaje"] = new SelectList(_context.TipoHospedajes, "IdTipoHospedaje", "Nombre", empresaHospedaje?.IdTipoHospedaje);
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
