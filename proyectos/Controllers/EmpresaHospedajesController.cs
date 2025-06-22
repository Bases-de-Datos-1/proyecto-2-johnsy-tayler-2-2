using HotelesCaribe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelesCaribe.Controllers
{
    [Authorize]
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

                var empresa = await _context.EmpresaHospedajes.FindAsync(id); //CAMBIAR DESPUES
                if (empresa != null)
                {
                    // Facebook (1)
                    if (!string.IsNullOrEmpty(modelo.Facebook))
                    {
                        var parameters = new[]
                        {
                            new SqlParameter("@p_idEmpresaHospedaje", modelo.IdEmpresa), new SqlParameter("@p_idTipoRed", 1), new SqlParameter("@p_url", modelo.Facebook),
                        };
                        await _context.Database.ExecuteSqlRawAsync(
                            "EXEC SP_InsertarRedSocial @p_idEmpresaHospedaje, @p_idTipoRed, @p_url", parameters);
                    }

                    // Instagram (2)
                    if (!string.IsNullOrEmpty(modelo.Instagram))
                    {
                        var parameters = new[]
                        {
                            new SqlParameter("@p_idEmpresaHospedaje", modelo.IdEmpresa), new SqlParameter("@p_idTipoRed", 2), new SqlParameter("@p_url", modelo.Instagram)
                        };
                        await _context.Database.ExecuteSqlRawAsync("EXEC SP_InsertarRedSocial @p_idEmpresaHospedaje, @p_idTipoRed, @p_url", parameters);
                    }

                    // YouTube (3)
                    if (!string.IsNullOrEmpty(modelo.YouTube))
                    {
                        var parameters = new[]
                        {
                            new SqlParameter("@p_idEmpresaHospedaje", modelo.IdEmpresa), new SqlParameter("@p_idTipoRed", 3), new SqlParameter("@p_url", modelo.YouTube)
                        };
                        await _context.Database.ExecuteSqlRawAsync("EXEC SP_InsertarRedSocial @p_idEmpresaHospedaje, @p_idTipoRed, @p_url", parameters);
                    }

                    // TikTok (4)
                    if (!string.IsNullOrEmpty(modelo.TikTok))
                    {
                        var parameters = new[]
                        {
                            new SqlParameter("@p_idEmpresaHospedaje", modelo.IdEmpresa), new SqlParameter("@p_idTipoRed", 4), new SqlParameter("@p_url", modelo.TikTok)
                        };
                        await _context.Database.ExecuteSqlRawAsync("EXEC SP_InsertarRedSocial @p_idEmpresaHospedaje, @p_idTipoRed, @p_url", parameters);
                    }

                    // Airbnb (5)
                    if (!string.IsNullOrEmpty(modelo.Airbnb))
                    {
                        var parameters = new[]
                        {
                            new SqlParameter("@p_idEmpresaHospedaje", modelo.IdEmpresa), new SqlParameter("@p_idTipoRed", 5), new SqlParameter("@p_url", modelo.Airbnb)
                        };
                        await _context.Database.ExecuteSqlRawAsync("EXEC SP_InsertarRedSocial @p_idEmpresaHospedaje, @p_idTipoRed, @p_url", parameters);
                    }

                    // Threads (6)
                    if (!string.IsNullOrEmpty(modelo.Threads))
                    {
                        var parameters = new[]
                        {
                            new SqlParameter("@p_idEmpresaHospedaje", modelo.IdEmpresa), new SqlParameter("@p_idTipoRed", 6), new SqlParameter("@p_url", modelo.Threads)
                        };
                        await _context.Database.ExecuteSqlRawAsync("EXEC SP_InsertarRedSocial @p_idEmpresaHospedaje, @p_idTipoRed, @p_url", parameters);
                    }

                    // X (7)
                    if (!string.IsNullOrEmpty(modelo.X))
                    {
                        var parameters = new[]
                        {
                            new SqlParameter("@p_idEmpresaHospedaje", modelo.IdEmpresa), new SqlParameter("@p_idTipoRed", 7), new SqlParameter("@p_url", modelo.X)
                        };
                        await _context.Database.ExecuteSqlRawAsync("EXEC SP_InsertarRedSocial @p_idEmpresaHospedaje, @p_idTipoRed, @p_url", parameters);
                    }
                }

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


        // POST: EmpresaHospedajes/AgregarImagenes/5 - Solo para archivos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarImagenes(int id, IFormFile nuevoArchivo)
        {
            if (nuevoArchivo != null && nuevoArchivo.Length > 0)
            {
                try
                {
                    if (true)//EsImagenValida(nuevoArchivo))
                    {
                        // Crear directorio si no existe
                        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "empresas", id.ToString());
                        if (!Directory.Exists(imagesPath))
                        {
                            Directory.CreateDirectory(imagesPath);
                        }

                        var extension = Path.GetExtension(nuevoArchivo.FileName);
                        var nombreArchivo = $"{Guid.NewGuid()}{extension}";
                        var rutaCompleta = Path.Combine(imagesPath, nombreArchivo);
                        var rutaRelativa = $"/images/empresas/{id}/{nombreArchivo}";

                        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                        {
                            await nuevoArchivo.CopyToAsync(stream);
                        }

                        // Guardar en base de datos
                        var parameters = new[]
                        {
                    new SqlParameter("@idEmpresaHospedaje", id),
                    new SqlParameter("@rutaLocal", rutaRelativa)
                };

                        await _context.Database.ExecuteSqlRawAsync(
                            "EXEC SP_InsertarFotoEmpresaHospedaje @idEmpresaHospedaje, @rutaLocal",
                            parameters);

                        TempData["Success"] = "Imagen subida correctamente.";
                    }
                    else
                    {
                        TempData["Error"] = "El archivo no es válido. Solo se permiten imágenes JPG, PNG, GIF, WEBP de máximo 5MB.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error al subir la imagen: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Debe seleccionar un archivo.";
            }

            return RedirectToAction("AgregarImagenes", new { id = id });
        }

        // POST: EmpresaHospedajes/EliminarImagen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarImagen(int empresaId, int fotoId)
        {
            try
            {
                var foto = await _context.FotosEmpresaHospedajes
                    .FirstOrDefaultAsync(f => f.IdFoto == fotoId && f.IdEmpresaHospedaje == empresaId);

                if (foto != null)
                {
                    var rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", foto.RutaLocal.TrimStart('/'));
                    if (System.IO.File.Exists(rutaCompleta))
                    {
                        System.IO.File.Delete(rutaCompleta);
                    }

                    // Eliminar de la base de datos
                    _context.FotosEmpresaHospedajes.Remove(foto);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Imagen eliminada correctamente.";
                }
                else
                {
                    TempData["Error"] = "No se encontró la imagen a eliminar.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar la imagen: {ex.Message}";
            }

            return RedirectToAction("AgregarImagenes", new { id = empresaId });
        }

        // POST: EmpresaHospedajes/ContinuarPaso
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContinuarPaso(int empresaId)
        {
            return RedirectToAction("PanelControl", new { id = empresaId });
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
        [Authorize]
        public IActionResult Create()
        {
            ViewData["IdTipoHospedaje"] = new SelectList(_context.TipoHospedajes, "IdTipoHospedaje", "Nombre");
            return View();
        }

        // POST: EmpresaHospedajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpresaHospedaje,Nombre,CedulaJuridica,IdTipoHospedaje,Provincia,Canton,Distrito,Barrio,Senas,Latitud,Longitud,Correo")] EmpresaHospedaje empresaHospedaje)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@p_nombre", empresaHospedaje.Nombre),
                        new SqlParameter("@p_cedulaJuridica", empresaHospedaje.CedulaJuridica),
                        new SqlParameter("@p_idTipoHospedaje", empresaHospedaje.IdTipoHospedaje),
                        new SqlParameter("@p_provincia", empresaHospedaje.Provincia),
                        new SqlParameter("@p_canton", empresaHospedaje.Canton),
                        new SqlParameter("@p_distrito", empresaHospedaje.Distrito),
                        new SqlParameter("@p_barrio", empresaHospedaje.Barrio),
                        new SqlParameter("@p_senas", empresaHospedaje.Senas),
                        new SqlParameter("@p_latitud", empresaHospedaje.Latitud),
                        new SqlParameter("@p_longitud", empresaHospedaje.Longitud),
                        new SqlParameter("@p_correo", empresaHospedaje.Correo)
                    };

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_InsertarEmpresaHospedaje @p_nombre, @p_cedulaJuridica, @p_idTipoHospedaje, @p_provincia, @p_canton, @p_distrito, @p_barrio, @p_senas, @p_latitud, @p_longitud, @p_correo",
                        parameters);


                    var paramCedula = new SqlParameter("@cedulaJuridica", empresaHospedaje.CedulaJuridica);
                    var info = _context.VwInfoCompletaHospedajes
                        .FromSqlRaw("EXEC SP_InfoCompletaHospedaje_PorCedula @cedulaJuridica", paramCedula)
                        .AsEnumerable()
                        .FirstOrDefault();

                    if (info != null)
                    {
                        return RedirectToAction("RedesSociales", new { id = info.IdEmpresaHospedaje });
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo obtener el ID del hotel recién creado.");
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores, por ejemplo, mostrar un mensaje al usuario
                    ModelState.AddModelError("", $"Error al crear la empresa de hospedaje: {ex.Message}");
                }
            }
            else
            {
                var errores = new List<string>();

                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        errores.Add($"Error en '{key}': {error.ErrorMessage}");
                    }
                }

                foreach (var mensaje in errores)
                {
                    ModelState.AddModelError("", mensaje);
                }
            }
            ViewData["IdTipoHospedaje"] = new SelectList(_context.TipoHospedajes, "IdTipoHospedaje", "Nombre", empresaHospedaje?.IdTipoHospedaje);
            return View(empresaHospedaje);
        }

        // GET: EmpresaHospedajes/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
