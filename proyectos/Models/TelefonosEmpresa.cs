using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class TelefonosEmpresa
{
    public int IdTelefono { get; set; }

    public int IdEmpresaHospedaje { get; set; }

    public string Numero { get; set; } = null!;

    public virtual EmpresaHospedaje IdEmpresaHospedajeNavigation { get; set; } = null!;
}
