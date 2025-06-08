using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwActividadesRecreativa
{
    public string Empresa { get; set; } = null!;

    public string Encargado { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string TipoActividad { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public string? ServiciosIncluidos { get; set; }
}
