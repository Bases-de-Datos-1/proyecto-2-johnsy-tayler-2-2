using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class TipoRedSocial
{
    public int IdTipoRed { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Rede> Redes { get; set; } = new List<Rede>();
}
