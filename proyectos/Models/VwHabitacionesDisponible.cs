using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwHabitacionesDisponible
{
    public string NombreHotel { get; set; } = null!;

    public string TipoHabitacion { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string TipoCama { get; set; } = null!;

    public decimal Precio { get; set; }

    public int NumeroHabitacion { get; set; }

    public string? Comodidades { get; set; }
}
