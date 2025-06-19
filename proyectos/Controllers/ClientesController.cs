using HotelesCaribe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotelesCaribe.Controllers
{
    public class ClientesController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public ClientesController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            var modelo = new ModeloClienteConTelefonos();
            return View(modelo);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModeloClienteConTelefonos clienteConTelefonos)
        {
            if (ModelState.IsValid)
            {
                Cliente c = clienteConTelefonos.Cliente;
                var parameters = new[]
                    {
                        new SqlParameter("@p_nombre", c.Nombre),
                        new SqlParameter("@p_apellido1", c.Apellido1),
                        new SqlParameter("@p_apellido2", c.Apellido2),
                        new SqlParameter("@p_fechaNacimiento", c.FechaNacimiento),
                        new SqlParameter("@p_tipoIdentIFicacion", c.TipoIdentificacion),
                        new SqlParameter("@p_identIFicacion", c.Identificacion),
                        new SqlParameter("@p_pais", c.Pais),
                        new SqlParameter("@p_provincia", c.Provincia),
                        new SqlParameter("@p_canton", c.Canton),
                        new SqlParameter("@p_distrito", c.Distrito),
                        new SqlParameter("@p_correo", c.Correo)
                    };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_InsertarCliente @p_nombre, @p_apellido1, @p_apellido2, @p_fechaNacimiento, @p_tipoIdentIFicacion, @p_identIFicacion, @p_pais, @p_provincia, @p_canton, @p_distrito, @p_correo",
                    parameters);

                var paramCedula = new SqlParameter("@cedula", c.Identificacion);
                var info = _context.Clientes
                    .FromSqlRaw("SELECT * FROM Cliente WHERE Identificacion = @cedula", paramCedula)
                    .AsEnumerable()
                    .FirstOrDefault();

                var paramId = new SqlParameter("@p_idCliente", info?.IdCliente);
                if (!string.IsNullOrEmpty(clienteConTelefonos.Telefono1))
                {
                    var tel = new SqlParameter("@p_numero", clienteConTelefonos.Telefono1);
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_InsertarTelefonoCliente @p_idCliente, @p_numero", 
                        paramId, tel);
                }
                if (!string.IsNullOrEmpty(clienteConTelefonos.Telefono2))
                {
                    var tel = new SqlParameter("@p_numero", clienteConTelefonos.Telefono2);
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_InsertarTelefonoCliente @p_idCliente, @p_numero",
                        paramId, tel);
                }
                if (!string.IsNullOrEmpty(clienteConTelefonos.Telefono3))
                {
                    var tel = new SqlParameter("@p_numero", clienteConTelefonos.Telefono3);
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_InsertarTelefonoCliente @p_idCliente, @p_numero",
                        paramId, tel);
                }

                return RedirectToAction(nameof(CodigoVerificacion));
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
                    Debug.WriteLine(mensaje);
                    ModelState.AddModelError("", mensaje);
                }
            }
            return View(clienteConTelefonos);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Nombre,Apellido1,Apellido2,FechaNacimiento,TipoIdentIficacion,IdentIficacion,Pais,Provincia,Canton,Distrito,Correo")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }

        public IActionResult CodigoVerificacion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CodigoVerificacion(string codigo)
        {
            if (codigo == "123456")
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Código incorrecto");
            return View();
        }
    }
}
