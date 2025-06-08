using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwHabitacionesConComodidade
{
    public int IdHabitacion { get; set; }

    public string Hotel { get; set; } = null!;

    public string TipoHabitacion { get; set; } = null!;

    public decimal Precio { get; set; }

    public string? Comodidades { get; set; }
}
