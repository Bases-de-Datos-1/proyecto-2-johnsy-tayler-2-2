using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class TipoServicioHospedaje
{
    public int IdTipoServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ServiciosHospedaje> ServiciosHospedajes { get; set; } = new List<ServiciosHospedaje>();
}
