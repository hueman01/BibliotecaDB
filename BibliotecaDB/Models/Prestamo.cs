using System;
using System.Collections.Generic;

namespace BibliotecaDB.Models;

public partial class Prestamo
{
    public int IdPrestamo { get; set; }

    public int? IdCliente { get; set; }

    public int? IdLibro { get; set; }

    public DateTime? FechaPrestamo { get; set; }

    public DateTime? FechaDevolucionPrevista { get; set; }

    public string? Estado { get; set; }

    public int? IdCategoria { get; set; }

    public string? Titulo { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
