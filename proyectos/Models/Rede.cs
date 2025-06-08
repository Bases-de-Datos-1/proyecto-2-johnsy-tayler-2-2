using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class Rede
{
    public int IdRed { get; set; }

    public int IdEmpresaHospedaje { get; set; }

    public int IdTipoRed { get; set; }

    public string Url { get; set; } = null!;

    public virtual EmpresaHospedaje IdEmpresaHospedajeNavigation { get; set; } = null!;

    public virtual TipoRedSocial IdTipoRedNavigation { get; set; } = null!;
}
