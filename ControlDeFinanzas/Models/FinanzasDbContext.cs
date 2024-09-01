using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ControlDeFinanzas.Models;

public partial class FinanzasDbContext : DbContext
{
    public FinanzasDbContext()
    {
    }

    public FinanzasDbContext(DbContextOptions<FinanzasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Balance> Balances { get; set; }

    public virtual DbSet<Gasto> Gastos { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Balance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Balance__3214EC07A87546A8");

            entity.ToTable("Balance");

            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gastos__3214EC077969F334");

            entity.Property(e => e.Categoria).HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingresos__3214EC0714A94377");

            entity.Property(e => e.Categoria).HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
