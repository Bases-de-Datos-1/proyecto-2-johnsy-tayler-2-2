﻿using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class EmpresaHospedaje
{
    public int IdEmpresaHospedaje { get; set; }

    public string Nombre { get; set; } = null!;

    public string CedulaJuridica { get; set; } = null!;

    public int IdTipoHospedaje { get; set; }

    public string Provincia { get; set; } = null!;

    public string Canton { get; set; } = null!;

    public string Distrito { get; set; } = null!;

    public string? Barrio { get; set; }

    public string Senas { get; set; } = null!;

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<FotosEmpresaHospedaje> FotosEmpresaHospedajes { get; set; } = new List<FotosEmpresaHospedaje>();

    public virtual ICollection<Habitacion> Habitacions { get; set; } = new List<Habitacion>();

    public virtual TipoHospedaje? IdTipoHospedajeNavigation { get; set; }

    public virtual ICollection<Rede> Redes { get; set; } = new List<Rede>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<ServiciosHospedaje> ServiciosHospedajes { get; set; } = new List<ServiciosHospedaje>();

    public virtual ICollection<TelefonosEmpresa> TelefonosEmpresas { get; set; } = new List<TelefonosEmpresa>();

    public virtual ICollection<TipoHabitacion> TipoHabitacions { get; set; } = new List<TipoHabitacion>();

    public virtual ICollection<UsuarioEmpresa> UsuarioEmpresas { get; set; } = new List<UsuarioEmpresa>();
}
