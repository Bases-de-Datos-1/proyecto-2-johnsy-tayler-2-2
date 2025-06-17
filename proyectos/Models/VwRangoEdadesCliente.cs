using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwRangoEdadesCliente
{
    public int IdCliente { get; set; }

    public string Cliente { get; set; } = null!;

    public int? Edad { get; set; }
}
