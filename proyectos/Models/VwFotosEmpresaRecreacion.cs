using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwFotosEmpresaRecreacion
{
    public int IdFoto { get; set; }

    public int IdEmpresaRecreacion { get; set; }

    public string RutaLocal { get; set; } = null!;
}
