using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class UsuarioEmpresa
{
    public int IdUsuarioEmpresa { get; set; }

    public int IdUsuario { get; set; }

    public int IdEmpresaHospedaje { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public bool? Activo { get; set; }

    public virtual EmpresaHospedaje IdEmpresaHospedajeNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
