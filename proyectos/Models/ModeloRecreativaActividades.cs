using System.ComponentModel.DataAnnotations;

namespace HotelesCaribe.Models
{
    public class ModeloRecreativaActividades
    {
        public int IdEmpresa { get; set; }
        public List<ActividadViewModel> Actividades { get; set; } = new List<ActividadViewModel>();
    }

    public class ActividadViewModel
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }
    }
}