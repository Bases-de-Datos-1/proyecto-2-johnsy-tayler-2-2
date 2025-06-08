using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class TipoHospedaje
{
    public int IdTipoHospedaje { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<EmpresaHospedaje> EmpresaHospedajes { get; set; } = new List<EmpresaHospedaje>();
}
