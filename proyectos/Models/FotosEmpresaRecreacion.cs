using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class FotosEmpresaRecreacion
{
    public int IdFoto { get; set; }

    public int IdEmpresaRecreacion { get; set; }

    public string RutaLocal { get; set; } = null!;

    public virtual EmpresaRecreacion IdEmpresaRecreacionNavigation { get; set; } = null!;
}
