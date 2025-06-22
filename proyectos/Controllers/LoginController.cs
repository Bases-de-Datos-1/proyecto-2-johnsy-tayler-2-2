using HotelesCaribe.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace HotelesCaribe.Controllers
{
    public class LoginController : Controller
    {
        private readonly GestionHoteleraContext _context;

        public LoginController(GestionHoteleraContext context)
        {
            _context = context;
        }

        // GET: /Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Usar stored procedure para verificar usuario
                    var parametros = new[]
                    {
                        new SqlParameter("@nombreUsuario", model.Correo), // Usando correo como nombre de usuario
                        new SqlParameter("@contrasena", model.Password)
                    };

                    var resultado = await _context.Database
                        .SqlQueryRaw<VerificarUsuarioResult>(
                            "EXEC SP_VerificarContrasena @nombreUsuario, @contrasena",
                            parametros)
                        .ToListAsync();

                    var loginResult = resultado.FirstOrDefault();

                    if (loginResult == null || loginResult.LoginExitoso == 0)
                    {
                        ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos");
                        return View(model);
                    }

                    // Buscar cliente asociado (si existe)
                    var cliente = await _context.Clientes
                        .FirstOrDefaultAsync(c => c.Correo.ToLower() == model.Correo.ToLower());

                    // Crear claims con información del usuario y cliente
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginResult.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Name, loginResult.NombreUsuario),
                        new Claim(ClaimTypes.Email, model.Correo),
                        new Claim(ClaimTypes.Role, loginResult.Rol),
                        new Claim("UsuarioId", loginResult.IdUsuario.ToString()),
                        new Claim("NombreUsuario", loginResult.NombreUsuario),
                        new Claim("Rol", loginResult.Rol)
                    };

                    // Si hay cliente asociado, agregar claims del cliente
                    if (cliente != null)
                    {
                        claims.AddRange(new[]
                        {
                            new Claim("ClienteId", cliente.IdCliente.ToString()),
                            new Claim("ClienteNombre", cliente.Nombre),
                            new Claim("ClienteApellido1", cliente.Apellido1),
                            new Claim("ClienteApellido2", cliente.Apellido2 ?? ""),
                            new Claim("ClienteIdentificacion", cliente.Identificacion),
                            new Claim("FullName", $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}".Trim())
                        });
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = model.RememberMe ?
                            DateTimeOffset.UtcNow.AddDays(30) :
                            DateTimeOffset.UtcNow.AddHours(24)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Guardar información en sesión
                    await GuardarInformacionEnSession(loginResult, cliente);

                    // Redirigir según el rol
                    if (loginResult.Rol == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al iniciar sesión. Inténtelo de nuevo.");
                    Debug.WriteLine($"Error en login: {ex.Message}");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: /Login/Logout
        public async Task<IActionResult> Logout()
        {
            // Limpiar session
            HttpContext.Session.Clear();

            // Cerrar sesión de cookies
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        // Método para guardar información en session
        private async Task GuardarInformacionEnSession(VerificarUsuarioResult usuario, Cliente? cliente)
        {
            try
            {
                // Información del usuario
                var usuarioInfo = new
                {
                    IdUsuario = usuario.IdUsuario,
                    NombreUsuario = usuario.NombreUsuario,
                    Rol = usuario.Rol
                };

                HttpContext.Session.SetString("UsuarioInfo", JsonSerializer.Serialize(usuarioInfo));
                HttpContext.Session.SetInt32("UsuarioId", usuario.IdUsuario);
                HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
                HttpContext.Session.SetString("Rol", usuario.Rol);

                // Si hay cliente asociado
                if (cliente != null)
                {
                    var clienteInfo = new
                    {
                        Id = cliente.IdCliente,
                        Nombre = cliente.Nombre,
                        Apellido1 = cliente.Apellido1,
                        Apellido2 = cliente.Apellido2,
                        Correo = cliente.Correo,
                        Identificacion = cliente.Identificacion,
                        TipoIdentificacion = cliente.TipoIdentificacion,
                        NombreCompleto = $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}".Trim()
                    };

                    HttpContext.Session.SetString("ClienteInfo", JsonSerializer.Serialize(clienteInfo));
                    HttpContext.Session.SetInt32("ClienteId", cliente.IdCliente);

                    // Obtener teléfonos del cliente
                    var telefonos = await _context.TelefonosClientes
                        .Where(t => t.IdCliente == cliente.IdCliente)
                        .Select(t => t.Numero)
                        .ToListAsync();

                    if (telefonos.Any())
                    {
                        HttpContext.Session.SetString("ClienteTelefonos", JsonSerializer.Serialize(telefonos));
                    }
                }

                // Si es admin, obtener hoteles administrados
                if (usuario.Rol == "admin")
                {
                    var hotelesParams = new[]
                    {
                        new SqlParameter("@idUsuario", usuario.IdUsuario)
                    };

                    var hoteles = await _context.Database
                        .SqlQueryRaw<HotelesUsuarioResult>(
                            "EXEC SP_MostrarHotelesUsuario @idUsuario",
                            hotelesParams)
                        .ToListAsync();

                    if (hoteles.Any())
                    {
                        HttpContext.Session.SetString("HotelesAdmin", JsonSerializer.Serialize(hoteles));
                    }
                }

                HttpContext.Session.SetString("UltimaActividad", DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar información en session: {ex.Message}");
            }
        }

        // Método para obtener el usuario actual
        public Usuario? GetCurrentUsuario()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var usuarioIdClaim = User.FindFirst("UsuarioId")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(usuarioIdClaim, out int id))
                {
                    return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
                }
            }
            return null;
        }

        // Método para obtener el cliente actual (si existe)
        public Cliente? GetCurrentCliente()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var clienteIdClaim = User.FindFirst("ClienteId")?.Value;

                if (int.TryParse(clienteIdClaim, out int id))
                {
                    return _context.Clientes.FirstOrDefault(c => c.IdCliente == id);
                }
            }
            return null;
        }
    }

    // Clases para los resultados de los stored procedures
    public class VerificarUsuarioResult
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int LoginExitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }

    public class VerificarExisteResult
    {
        public int Existe { get; set; }
    }

    public class HotelesUsuarioResult
    {
        public int IdEmpresaHospedaje { get; set; }
        public string NombreHotel { get; set; } = string.Empty;
        public string CedulaJuridica { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public string TipoHospedaje { get; set; } = string.Empty;
        public DateTime FechaAsignacion { get; set; }
        public int TotalHabitaciones { get; set; }
    }

    // Clase para información del usuario en session
    public class UsuarioSessionInfo
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}