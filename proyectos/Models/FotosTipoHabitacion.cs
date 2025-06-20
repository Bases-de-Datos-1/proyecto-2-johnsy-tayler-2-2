using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class FotosTipoHabitacion
{
    public int IdFoto { get; set; }

    public int IdTipoHabitacion { get; set; }

    public string RutaLocal { get; set; } = null!;

    public virtual TipoHabitacion IdTipoHabitacionNavigation { get; set; } = null!;
}
