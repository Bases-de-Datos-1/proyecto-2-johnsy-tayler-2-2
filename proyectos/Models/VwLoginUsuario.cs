using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwLoginUsuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
