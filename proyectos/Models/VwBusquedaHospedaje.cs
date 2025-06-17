using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwBusquedaHospedaje
{
    public int IdEmpresaHospedaje { get; set; }

    public string NombreHotel { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public string Canton { get; set; } = null!;

    public string Distrito { get; set; } = null!;

    public string Senas { get; set; } = null!;

    public string TipoHospedaje { get; set; } = null!;

    public string? Correo { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? Servicios { get; set; }

    public decimal? PrecioMinimo { get; set; }
}
