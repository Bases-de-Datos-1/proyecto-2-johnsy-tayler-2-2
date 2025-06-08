using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int IdReserva { get; set; }

    public DateTime Fecha { get; set; }

    public decimal ImporteTotal { get; set; }

    public string FormaPaGo { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
