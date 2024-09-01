using System;
using System.Collections.Generic;

namespace ControlDeFinanzas.Models;

public partial class Balance
{
    public int Id { get; set; }

    public decimal MontoTotal { get; set; }

    public DateOnly FechaActualizacion { get; set; }
}
