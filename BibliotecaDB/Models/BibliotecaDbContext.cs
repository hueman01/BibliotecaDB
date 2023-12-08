using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDB.Models;

public partial class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext()
    {
    }

    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-C49AKPBM; Database=BibliotecaDB; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK_Autores_1");

            entity.Property(e => e.IdAutor).HasColumnName("ID_autor");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("Fecha_nacimiento");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Nombre_autor");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Nombre_categoria");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__37900156593AB962");

            entity.Property(e => e.IdCategoria).HasColumnName("ID_categoria");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Nombre_categoria");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__9BB2655BD6666984");

            entity.Property(e => e.IdCliente).HasColumnName("ID_cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Correo_electronico");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__FC29D6F6F346EE7A");

            entity.Property(e => e.IdEmpleado).HasColumnName("ID_empleado");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Cargo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("date")
                .HasColumnName("Fecha_contratacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.IdIngreso).HasName("PK__Ingresos__B68DFB0B1650B89E");

            entity.Property(e => e.IdIngreso).HasColumnName("ID_ingreso");
            entity.Property(e => e.CantidadIngresada).HasColumnName("Cantidad_ingresada");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("date")
                .HasColumnName("Fecha_ingreso");
            entity.Property(e => e.IdLibro)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_libro");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Ingresos__ID_lib__1BFD2C07");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__Inventar__078A0B829B89CBE8");

            entity.ToTable("Inventario");

            entity.Property(e => e.IdInventario).HasColumnName("ID_inventario");
            entity.Property(e => e.CantidadDisponible).HasColumnName("Cantidad_disponible");
            entity.Property(e => e.CantidadTotal).HasColumnName("Cantidad_total");
            entity.Property(e => e.IdLibro)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_libro");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Inventari__ID_li__267ABA7A");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__Libros__4E7BBBD518FCD829");

            entity.Property(e => e.IdLibro)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ID_libro");
            entity.Property(e => e.AnioPublicacion).HasColumnName("Anio_publicacion");
            entity.Property(e => e.CantidadDisponible).HasColumnName("Cantidad_disponible");
            entity.Property(e => e.IdAutor).HasColumnName("ID_autor");
            entity.Property(e => e.IdCategoria).HasColumnName("ID_categoria");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Nombre_autor");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("FK_Libros_Autores");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Libros_Categorias");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo).HasName("PK__Prestamo__068A37F34D0AB48D");

            entity.Property(e => e.IdPrestamo).HasColumnName("ID_prestamo");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaDevolucionPrevista)
                .HasColumnType("date")
                .HasColumnName("Fecha_devolucion_prevista");
            entity.Property(e => e.FechaPrestamo)
                .HasColumnType("date")
                .HasColumnName("Fecha_prestamo");
            entity.Property(e => e.IdCategoria).HasColumnName("ID_categoria");
            entity.Property(e => e.IdCliente).HasColumnName("ID_cliente");
            entity.Property(e => e.IdLibro).HasColumnName("ID_libro");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Prestamos_Categorias");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Prestamos__ID_cl__182C9B23");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__182A541237E6E25B");

            entity.Property(e => e.IdRol).HasColumnName("ID_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Nombre_rol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_usuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdEmpleado).HasColumnName("ID_empleado");
            entity.Property(e => e.IdRol).HasColumnName("ID_rol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Nombre_usuario");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Usuarios__ID_emp__22AA2996");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuarios__ID_rol__239E4DCF");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Prestamos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
