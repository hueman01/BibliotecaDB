using System;
using System.Collections.Generic;

namespace BibliotecaDB.Models;

public partial class Autore
{
    public int IdAutor { get; set; }

    public string? NombreAutor { get; set; }

    public string? Nacionalidad { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Titulo { get; set; }

    public string? NombreCategoria { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
