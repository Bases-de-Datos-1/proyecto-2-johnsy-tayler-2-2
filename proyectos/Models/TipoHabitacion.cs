using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class TipoHabitacion
{
    public int IdTipo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string TipoCama { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual ICollection<Comodidade> Comodidades { get; set; } = new List<Comodidade>();

    public virtual ICollection<FotosTipoHabitacion> FotosTipoHabitacions { get; set; } = new List<FotosTipoHabitacion>();

    public virtual ICollection<Habitacion> Habitacions { get; set; } = new List<Habitacion>();
}
