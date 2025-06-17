using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwReservasFinalizadasPorTipo
{
    public string TipoHabitacion { get; set; } = null!;

    public int? TotalReservas { get; set; }

    public DateTime? PrimeraFecha { get; set; }

    public DateOnly? UltimaFecha { get; set; }
}
