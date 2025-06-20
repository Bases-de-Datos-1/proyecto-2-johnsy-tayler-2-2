using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwAdminsPorHotel
{
    public int IdEmpresaHospedaje { get; set; }

    public string NombreHotel { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public string Canton { get; set; } = null!;

    public int? TotalAdmins { get; set; }

    public string? Administradores { get; set; }
}
