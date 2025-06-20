using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwInfoCompletaHospedaje
{

    public int IdEmpresaHospedaje { get; set; }

    public string NombreHotel { get; set; } = null!;

    public string CedulaJuridica { get; set; } = null!;

    public string TipoHospedaje { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public string Canton { get; set; } = null!;

    public string Distrito { get; set; } = null!;

    public string? Barrio { get; set; }

    public string Senas { get; set; } = null!;

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? Correo { get; set; }

    public string? Servicios { get; set; }

    public string? RedesSociales { get; set; }
}
