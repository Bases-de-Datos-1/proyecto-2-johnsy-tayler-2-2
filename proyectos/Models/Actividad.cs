using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class Actividad
{
    public int IdActividad { get; set; }

    public int IdTipoActividad { get; set; }

    public int IdEmpresaRecreacion { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual EmpresaRecreacion IdEmpresaRecreacionNavigation { get; set; } = null!;

    public virtual TipoActividad IdTipoActividadNavigation { get; set; } = null!;
}
