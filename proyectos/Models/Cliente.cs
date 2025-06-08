using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string TipoIdentIficacion { get; set; } = null!;

    public string IdentIficacion { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string? Provincia { get; set; }

    public string? Canton { get; set; }

    public string? Distrito { get; set; }

    public string Correo { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<TelefonosCliente> TelefonosClientes { get; set; } = new List<TelefonosCliente>();
}
