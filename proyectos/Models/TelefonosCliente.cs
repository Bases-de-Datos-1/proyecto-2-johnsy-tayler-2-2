using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class TelefonosCliente
{
    public int IdTelefono { get; set; }

    public int IdCliente { get; set; }

    public string Numero { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
