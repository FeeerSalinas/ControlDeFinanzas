using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDeFinanzas.Models;

public partial class Gasto
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public DateOnly Fecha { get; set; }

    public string? Descripcion { get; set; }

    public string? Categoria { get; set; }

    [NotMapped]
    public decimal SumaEducacion { get; set; }

    [NotMapped]
    public decimal SumaSuscripcion { get; set; }

    [NotMapped]
    public decimal SumaEmergencia { get; set; }

    [NotMapped]
    public decimal SumaEntretenimiento { get; set; }

    [NotMapped]
    public decimal SumaSalud { get; set; }

    [NotMapped]
    public decimal Total { get; set; }

}
