using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class FotosHabitacion
{
    public int IdFoto { get; set; }

    public int IdTipoHabitacion { get; set; }

    public string Url { get; set; } = null!;

    public virtual TipoHabitacion IdTipoHabitacionNavigation { get; set; } = null!;
}
