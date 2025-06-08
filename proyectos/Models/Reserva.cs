using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int IdCliente { get; set; }

    public int IdEmpresaHospedaje { get; set; }

    public int IdHabitacion { get; set; }

    public DateTime FechaIngreso { get; set; }

    public int CantidadPersonAs { get; set; }

    public bool TieneVehiculo { get; set; }

    public DateOnly FechAsalida { get; set; }

    public TimeOnly HorAsalida { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual EmpresaHospedaje IdEmpresaHospedajeNavigation { get; set; } = null!;

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;
}
