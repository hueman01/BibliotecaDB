using System;
using System.Collections.Generic;

namespace BibliotecaDB.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdEmpleado { get; set; }

    public string? Usuario1 { get; set; }

    public string? Contrasena { get; set; }

    public int? IdRol { get; set; }

    public string? NombreUsuario { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual Prestamo IdUsuarioNavigation { get; set; } = null!;
}
