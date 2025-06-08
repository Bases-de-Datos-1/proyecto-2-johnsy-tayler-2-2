using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class ServiciosHospedaje
{
    public int IdServicio { get; set; }

    public int IdEmpresaHospedaje { get; set; }

    public int IdTipoServicio { get; set; }

    public virtual EmpresaHospedaje IdEmpresaHospedajeNavigation { get; set; } = null!;

    public virtual TipoServicioHospedaje IdTipoServicioNavigation { get; set; } = null!;
}
