using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class TipoActividad
{
    public int IdTipoActividad { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Actividad> Actividads { get; set; } = new List<Actividad>();
}
