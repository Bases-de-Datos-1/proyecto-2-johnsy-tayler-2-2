using System.ComponentModel.DataAnnotations;

namespace HotelesCaribe.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Por favor ingrese un correo electrónico válido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }

        //public string ReturnUrl { get; set; }
    }

    // Modelo para el resultado del login
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        //public string RedirectUrl { get; set; }
        public Cliente Cliente { get; set; }
    }

    // Si necesitas un modelo más completo para autenticación
    public class AuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }

        public AuthenticateResponse(Cliente cliente, string token, DateTime tokenExpiration)
        {
            Id = cliente.IdCliente;
            Nombre = cliente.Nombre;
            Apellido1 = cliente.Apellido1;
            Apellido2 = cliente.Apellido2;
            Correo = cliente.Correo;
            Token = token;
            TokenExpiration = tokenExpiration;
        }
    }
}