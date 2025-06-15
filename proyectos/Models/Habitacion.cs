using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class Habitacion
{
    public int IdHabitacion { get; set; }

    public int IdEmpresaHospedaje { get; set; }

    public int Numero { get; set; }

    public int IdTipoHabitacion { get; set; }

    public virtual EmpresaHospedaje? IdEmpresaHospedajeNavigation { get; set; }

    public virtual TipoHabitacion? IdTipoHabitacionNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
