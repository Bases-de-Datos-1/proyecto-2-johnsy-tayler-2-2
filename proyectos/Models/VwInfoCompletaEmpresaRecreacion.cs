using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwInfoCompletaEmpresaRecreacion
{
    public int IdEmpresaRecreacion { get; set; }

    public string Nombre { get; set; } = null!;

    public string CedulaJuridica { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Encargado { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public string Canton { get; set; } = null!;

    public string Distrito { get; set; } = null!;

    public string Senas { get; set; } = null!;

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? Servicios { get; set; }

    public string? Actividades { get; set; }
}
