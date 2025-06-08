using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class Comodidade
{
    public int IdComodidad { get; set; }

    public int IdTipoHabitacion { get; set; }

    public string Comodidad { get; set; } = null!;

    public virtual TipoHabitacion IdTipoHabitacionNavigation { get; set; } = null!;
}
