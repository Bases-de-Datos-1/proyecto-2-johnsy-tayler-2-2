using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwFotosTipoHabitacion
{
    public int IdFoto { get; set; }

    public int IdTipoHabitacion { get; set; }

    public string RutaLocal { get; set; } = null!;
}
