using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwFotosEmpresaHospedaje
{
    public int IdFoto { get; set; }

    public int IdEmpresaHospedaje { get; set; }

    public string RutaLocal { get; set; } = null!;
}
