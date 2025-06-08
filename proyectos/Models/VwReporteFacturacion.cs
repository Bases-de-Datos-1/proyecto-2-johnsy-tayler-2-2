using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwReporteFacturacion
{
    public int IdFactura { get; set; }

    public int IdReserva { get; set; }

    public string Cliente { get; set; } = null!;

    public string Hotel { get; set; } = null!;

    public int Habitacion { get; set; }

    public string TipoHabitacion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public decimal ImporteTotal { get; set; }

    public string FormaPaGo { get; set; } = null!;
}
