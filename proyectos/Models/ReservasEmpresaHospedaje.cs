using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class ReservasEmpresaHospedaje
{
    public int IdReservaHospedaje { get; set; }

    public int IdReserva { get; set; }

    public virtual Reserva IdReservaNavigation { get; set; } = null!;
}
