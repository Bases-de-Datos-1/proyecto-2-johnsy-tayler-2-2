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
    public class EmpresaRecreacionsController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public EmpresaRecreacionsController(GestionHoteleraContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpresaRecreacions.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@idEmpresaRecreacion", id);
            var empresaRecreacion = await _context.EmpresaRecreacions
                .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorId @idEmpresaRecreacion", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (empresaRecreacion == null)
            {
                return NotFound();
            }

            return View(empresaRecreacion);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpresaRecreacion,Nombre,CedulaJuridica,Correo,Telefono,Encargado,Provincia,Canton,Distrito,Senas,Latitud,Longitud")] EmpresaRecreacion empresaRecreacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@p_nombre", empresaRecreacion.Nombre),
                        new SqlParameter("@p_cedulaJuridica", empresaRecreacion.CedulaJuridica),
                        new SqlParameter("@p_correo", empresaRecreacion.Correo ?? (object)DBNull.Value),
                        new SqlParameter("@p_telefono", empresaRecreacion.Telefono ?? (object)DBNull.Value),
                        new SqlParameter("@p_encargado", empresaRecreacion.Encargado ?? (object)DBNull.Value),
                        new SqlParameter("@p_provincia", empresaRecreacion.Provincia),
                        new SqlParameter("@p_canton", empresaRecreacion.Canton),
                        new SqlParameter("@p_distrito", empresaRecreacion.Distrito),
                        new SqlParameter("@p_senas", empresaRecreacion.Senas),
                        new SqlParameter("@p_latitud", empresaRecreacion.Latitud ?? (object)DBNull.Value),
                        new SqlParameter("@p_longitud", empresaRecreacion.Longitud ?? (object)DBNull.Value)
                    };

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_InsertarEmpresaRecreacion @p_nombre, @p_cedulaJuridica, @p_correo, @p_telefono, @p_encargado, @p_provincia, @p_canton, @p_distrito, @p_senas, @p_latitud, @p_longitud",
                        parameters);

                    var paramCedula = new SqlParameter("@cedulaJuridica", empresaRecreacion.CedulaJuridica);
                    var info = _context.VwInfoCompletaEmpresaRecreacions
                        .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorCedula @cedulaJuridica", paramCedula)
                        .AsEnumerable()
                        .FirstOrDefault();

                    if (info != null)
                    {
                        return RedirectToAction("AgregarTiposServicios", new { id = info.IdEmpresaRecreacion });
                    }
                    else
                    {
                        ModelState.AddModelError("", "No se pudo obtener el ID del hotel recién creado.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al crear la empresa de recreación: {ex.Message}");
                }
            }
            return View(empresaRecreacion);
        }


        public async Task<IActionResult> AgregarTiposServicios(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@idEmpresaRecreacion", id);
            var empresaRecreacion = await _context.EmpresaRecreacions
                .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorId @idEmpresaRecreacion", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();
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
                var param = new SqlParameter("@idEmpresaRecreacion", id);
                var empresa = await _context.EmpresaRecreacions
                    .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorId @idEmpresaRecreacion", param)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
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


        public async Task<IActionResult> AgregarActividades(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@idEmpresaRecreacion", id);
            var empresaRecreacion = await _context.EmpresaRecreacions
                .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorId @idEmpresaRecreacion", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();
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
                var param = new SqlParameter("@idEmpresaRecreacion", id);
                var empresa = await _context.EmpresaRecreacions
                    .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorId @idEmpresaRecreacion", param)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                if (empresa != null)
                {

                    foreach (ModeloActividad actividad in modelo.Actividades)
                    {
                        if (!string.IsNullOrWhiteSpace(actividad.Nombre) && !string.IsNullOrWhiteSpace(actividad.Descripcion) && actividad.Precio >= 0)
                        {
                            if (ModelState.IsValid)
                            {
                                var parameters = new[]
                                {
                                    new SqlParameter("@p_idTipoActividad", 1),
                                    new SqlParameter("@p_idEmpresaRecreacion", id),
                                    new SqlParameter("@p_descripcion", actividad.Descripcion),
                                    new SqlParameter("@p_precio", actividad.Precio),
                                };

                                await _context.Database.ExecuteSqlRawAsync(
                                    "EXEC SP_InsertarEmpresaRecreacion @p_idTipoActividad, @p_idEmpresaRecreacion, @p_descripcion, @p_precio",
                                    parameters);
                            }
                               
                            
                            return View(modelo);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Todos los campos de las actividades son obligatorios y el precio debe ser mayor a 0.");
                            return View(modelo);
                        }
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction("PanelControlRecreativa", new {id = modelo.IdEmpresa});
                }
            }

            return View(modelo);
        }



        public async Task<IActionResult> PanelControlRecreativa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@idEmpresaRecreacion", id);
            var empresaRecreacion = await _context.EmpresaRecreacions
                .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorId @idEmpresaRecreacion", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();


            if (empresaRecreacion == null)
            {
                return NotFound();
            }

            return View(empresaRecreacion);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@idEmpresaRecreacion", id);
            var empresaRecreacion = await _context.EmpresaRecreacions
                .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorId @idEmpresaRecreacion", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (empresaRecreacion == null)
            {
                return NotFound();
            }
            return View(empresaRecreacion);
        }

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
                    var parameters = new[]
                    {
                        new SqlParameter("@idEmpresaRecreacion", empresaRecreacion.IdEmpresaRecreacion),
                        new SqlParameter("@nombre", empresaRecreacion.Nombre),
                        new SqlParameter("@cedulaJuridica", empresaRecreacion.CedulaJuridica),
                        new SqlParameter("@correo", empresaRecreacion.Correo ?? (object)DBNull.Value),
                        new SqlParameter("@telefono", empresaRecreacion.Telefono ?? (object)DBNull.Value),
                        new SqlParameter("@encargado", empresaRecreacion.Encargado ?? (object)DBNull.Value),
                        new SqlParameter("@provincia", empresaRecreacion.Provincia),
                        new SqlParameter("@canton", empresaRecreacion.Canton),
                        new SqlParameter("@distrito", empresaRecreacion.Distrito),
                        new SqlParameter("@senas", empresaRecreacion.Senas),
                        new SqlParameter("@latitud", empresaRecreacion.Latitud ?? (object)DBNull.Value),
                        new SqlParameter("@longitud", empresaRecreacion.Longitud ?? (object)DBNull.Value)
                    };

                    await _context.Database.ExecuteSqlRawAsync("EXEC SP_ActualizarEmpresaRecreacion @idEmpresaRecreacion, @nombre, @cedulaJuridica, @correo, @telefono, @encargado, @provincia, @canton, @distrito, @senas, @latitud, @longitud", parameters);

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var param = new SqlParameter("@idEmpresaRecreacion", id);
            var empresaRecreacion = await _context.EmpresaRecreacions
                .FromSqlRaw("EXEC SP_InfoEmpresaRecreacionPorId @idEmpresaRecreacion", param)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (empresaRecreacion == null)
            {
                return NotFound();
            }

            return View(empresaRecreacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var param = new SqlParameter("@idEmpresaRecreacion", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_EliminarEmpresaRecreacion @idEmpresaRecreacion", param);

            return RedirectToAction(nameof(Index));
        }


        private bool EmpresaRecreacionExists(int id)
        {
            return _context.EmpresaRecreacions.Any(e => e.IdEmpresaRecreacion == id);
        }
    }
}