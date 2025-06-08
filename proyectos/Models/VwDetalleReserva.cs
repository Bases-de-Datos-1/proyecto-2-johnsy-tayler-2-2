using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwDetalleReserva
{
    public int IdReserva { get; set; }

    public string Cliente { get; set; } = null!;

    public string Hotel { get; set; } = null!;

    public int Habitacion { get; set; }

    public string TipoHabitacion { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public DateOnly FechAsalida { get; set; }

    public TimeOnly HorAsalida { get; set; }

    public int CantidadPersonAs { get; set; }

    public string Vehiculo { get; set; } = null!;

    public int? Noches { get; set; }

    public decimal? TotalEstimado { get; set; }
}
