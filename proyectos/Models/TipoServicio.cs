using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class TipoServicio
{
    public int IdTipoServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
