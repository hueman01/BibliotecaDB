using System;
using System.Collections.Generic;

namespace BibliotecaDB.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public string? IdLibro { get; set; }

    public int? CantidadTotal { get; set; }

    public int? CantidadDisponible { get; set; }

    public virtual Libro? IdLibroNavigation { get; set; }
}
