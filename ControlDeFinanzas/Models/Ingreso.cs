using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDeFinanzas.Models;

public partial class Ingreso
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Descripcion { get; set; }

    public string? Categoria { get; set; }

    [NotMapped]
    public decimal SumaSalario { get; set; }

    [NotMapped]
    public decimal SumaVenta { get; set; }

    [NotMapped]
    public decimal SumaOtro { get; set; }
    [NotMapped]
    public decimal Total { get; set; }
}
