using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class FotosEmpresaHospedaje
{
    public int IdFoto { get; set; }

    public int IdEmpresaHospedaje { get; set; }

    public string Url { get; set; } = null!;

    public virtual EmpresaHospedaje IdEmpresaHospedajeNavigation { get; set; } = null!;
}
