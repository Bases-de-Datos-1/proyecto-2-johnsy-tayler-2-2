using System;
using System.ComponentModel.DataAnnotations;

namespace HotelesCaribe.Models
{
    public class ReservaComprobanteModel
    {
        [Display(Name = "Número de Reserva")]
        public string NumeroReserva { get; set; }

        [Display(Name = "Nombre del Hotel")]
        public string NombreHotel { get; set; }

        [Display(Name = "Identificación del Cliente")]
        public string IdentificacionCliente { get; set; }

        [Display(Name = "Fecha de Entrada")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrada { get; set; }

        [Display(Name = "Hora de Entrada")]
        [DataType(DataType.Time)]
        public TimeSpan HoraEntrada { get; set; }

        [Display(Name = "Fecha de Salida")]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; }

        [Display(Name = "Hora de Salida")]
        [DataType(DataType.Time)]
        public TimeSpan HoraSalida { get; set; }

        [Display(Name = "Número de Habitaciones")]
        public int NumeroHabitaciones { get; set; }

        [Display(Name = "Número de Personas")]
        public int NumeroPersonas { get; set; }

        [Display(Name = "Tiene Vehículo")]
        public bool TieneVehiculo { get; set; }

        [Display(Name = "Descripción de la Habitación")]
        public string DescripcionHabitacion { get; set; }

        [Display(Name = "Total a Pagar")]
        [DataType(DataType.Currency)]
        public decimal TotalPagar { get; set; }

        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public int IdHotel { get; set; }
        public int IdHabitacion { get; set; }

        [Display(Name = "Estado de la Reserva")]
        public string EstadoReserva { get; set; } = "Confirmada";

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}