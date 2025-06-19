using HotelesCaribe.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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
            //ViewBag.OcultarHeader = true;

            if (ModelState.IsValid)
            {

                try
                {
                    // Buscar cliente por correo
                    var cliente = _context.Clientes
                        .FirstOrDefault(c => c.Correo.ToLower() == model.Correo.ToLower());

                    if (cliente == null)
                    {
                        ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos");
                        return View(model);
                    }

                    if (!VerifyPassword(model.Password, "abc"))//cliente.Password))
                    {
                        ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos");
                        return View(model);
                    }

                    var claims = new List<Claim>
                    {
                        //new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()),
                        new Claim(ClaimTypes.Name, $"{cliente.Nombre} {cliente.Apellido1}"),
                        new Claim(ClaimTypes.Email, cliente.Correo),
                        new Claim("FullName", $"{cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}"),
                        //new Claim("ClienteId", cliente.Id.ToString())
                    };

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

                    
                    /*
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    */

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al iniciar sesión. Inténtelo de nuevo.");
                    ModelState.AddModelError("", ex.Message + ex.Source);
                    // Log the exception
                    return View(model);
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
                    Debug.WriteLine(mensaje);
                    ModelState.AddModelError("", mensaje);
                }
            }
            return View(model);
        }

        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return password == hashedPassword;
        }

        // Método para obtener el cliente actual
        private Cliente GetCurrentClient()
        {
            if (User.Identity.IsAuthenticated)
            {
                var clienteId = User.FindFirst("ClienteId")?.Value;
                if (int.TryParse(clienteId, out int id))
                {
                    return _context.Clientes.FirstOrDefault(c => c.IdCliente == id);
                }
            }
            return null;
        }
    }
}