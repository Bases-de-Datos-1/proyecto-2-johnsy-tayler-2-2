// Models/ReservaModel.cs
using System.ComponentModel.DataAnnotations;

namespace HotelesCaribe.Models
{
    public class ReservaModel
    {
        public int IdHabitacion { get; set; }
        public EmpresaHospedaje Hotel { get; set; }
        public TipoHabitacion TipoHabitacion { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "La fecha de entrada es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de entrada")]
        public DateTime FechaEntrada { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de salida")]
        public DateTime FechaSalida { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Hora de entrada")]
        public TimeSpan HoraEntrada { get; set; } = new TimeSpan(12, 0, 0); // 12:00 PM

        [DataType(DataType.Time)]
        [Display(Name = "Hora de salida")]
        public TimeSpan HoraSalida { get; set; } = new TimeSpan(12, 0, 0); // 12:00 PM

        [Required]
        [Range(1, 5, ErrorMessage = "Debe seleccionar entre 1 y 5 habitaciones")]
        [Display(Name = "Número de habitaciones")]
        public int NumeroHabitaciones { get; set; } = 1;

        [Required]
        [Range(1, 10, ErrorMessage = "Debe seleccionar entre 1 y 10 personas")]
        [Display(Name = "Número de personas")]
        public int NumeroPersonas { get; set; } = 1;

        [Display(Name = "Tiene vehículo")]
        public bool TieneVehiculo { get; set; }

        // Propiedades calculadas
        public int NumeroNoches => (FechaSalida - FechaEntrada).Days;
        public decimal PrecioTotal => NumeroNoches * TipoHabitacion?.Precio ?? 0 * NumeroHabitaciones;
    }
}