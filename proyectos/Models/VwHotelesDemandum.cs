using System;
using System.Collections.Generic;

namespace HotelesCaribe.Models;

public partial class VwHotelesDemandum
{
    public string Hotel { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public string Canton { get; set; } = null!;

    public int? TotalReservas { get; set; }
}
