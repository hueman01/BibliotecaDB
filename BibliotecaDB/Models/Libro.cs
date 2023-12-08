using System;
using System.Collections.Generic;

namespace BibliotecaDB.Models;

public partial class Libro
{
    public string IdLibro { get; set; } = null!;

    public string? Titulo { get; set; }

    public int? IdAutor { get; set; }

    public int? AnioPublicacion { get; set; }

    public int? CantidadDisponible { get; set; }

    public int? IdCategoria { get; set; }

    public string? NombreAutor { get; set; }

    public virtual Autore? IdAutorNavigation { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
