using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwHotelesUsuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public int IdEmpresaHospedaje { get; set; }

    public string NombreHotel { get; set; } = null!;

    public string CedulaJuridica { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public string Canton { get; set; } = null!;

    public string TipoHospedaje { get; set; } = null!;

    public DateTime? FechaAsignacion { get; set; }

    public bool? EsAdminActivo { get; set; }
}
