using System;
using System.Collections.Generic;

namespace BibliotecaDB.Models;

public partial class Ingreso
{
    public int IdIngreso { get; set; }

    public string? IdLibro { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public int? CantidadIngresada { get; set; }

    public virtual Libro? IdLibroNavigation { get; set; }
}
