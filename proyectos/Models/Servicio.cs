using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public int IdTipoServicio { get; set; }

    public int IdEmpresaRecreacion { get; set; }

    public virtual EmpresaRecreacion IdEmpresaRecreacionNavigation { get; set; } = null!;

    public virtual TipoServicio IdTipoServicioNavigation { get; set; } = null!;
}
