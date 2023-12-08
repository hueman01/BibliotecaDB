using System;
using System.Collections.Generic;

namespace BibliotecaDB.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
