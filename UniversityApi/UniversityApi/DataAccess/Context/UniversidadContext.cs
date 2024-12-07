using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UniversityApi.DataAccess;

namespace UniversityApi.DataAccess.Context;

public partial class UniversidadContext : DbContext
{
    public UniversidadContext()
    {
    }

    public UniversidadContext(DbContextOptions<UniversidadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Credito> Creditos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<MateriaProfesor> MateriaProfesors { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Profesor> Profesores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => new { e.ProfesorId, e.EstudianteId });

            entity.ToTable("Clase");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Clases)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clase_Estudiante");

            entity.HasOne(d => d.MateriaProfesor).WithMany(p => p.Clases)
                .HasForeignKey(d => new { d.MateriaId, d.ProfesorId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clase_MateriaProfesor");
        });

        modelBuilder.Entity<Credito>(entity =>
        {
            entity.ToTable("Credito");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.EstudianteId).HasName("PK_Estudiante_1");

            entity.ToTable("Estudiante");

            entity.Property(e => e.EstudianteId).ValueGeneratedNever();
            entity.Property(e => e.FechaInscrito).HasColumnType("smalldatetime");

            entity.HasOne(d => d.EstudianteNavigation).WithOne(p => p.Estudiante)
                .HasForeignKey<Estudiante>(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estudiante_Usuario");
        });

        modelBuilder.Entity<MateriaProfesor>(entity =>
        {
            entity.HasKey(e => new { e.MateriaId, e.ProfesorId }).HasName("PK_MateriaProfesor_1");

            entity.ToTable("MateriaProfesor");

            entity.HasOne(d => d.Materia).WithMany(p => p.MateriaProfesors)
                .HasForeignKey(d => d.MateriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MateriaProfesor_Materia");

            entity.HasOne(d => d.Profesor).WithMany(p => p.MateriaProfesors)
                .HasForeignKey(d => d.ProfesorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MateriaProfesor_Profesor");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.MateriaId);

            entity.ToTable("Materia");

            entity.Property(e => e.Titulo).HasMaxLength(250);
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.ProfesorId).HasName("PK_Profesor_1");

            entity.ToTable("Profesor");

            entity.Property(e => e.ProfesorId).ValueGeneratedNever();
            entity.Property(e => e.FechaContratacion).HasColumnType("smalldatetime");

            entity.HasOne(d => d.ProfesorNavigation).WithOne(p => p.Profesor)
                .HasForeignKey<Profesor>(d => d.ProfesorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profesor_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.HasIndex(e => e.NumeroIdentificacion, "UQ__Usuario__FCA68D9105274D61").IsUnique();

            entity.Property(e => e.Apellidos).HasMaxLength(200);
            entity.Property(e => e.Contrasena).HasMaxLength(100);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombres).HasMaxLength(200);
            entity.Property(e => e.NumeroIdentificacion).HasMaxLength(50);
            entity.Property(e => e.Salt).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
