using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiFirst.Models;

public partial class EmpDeptDbContext : DbContext
{
    public EmpDeptDbContext()
    {
    }

    public EmpDeptDbContext(DbContextOptions<EmpDeptDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentTbl> DepartmentTbls { get; set; }

    public virtual DbSet<EmployeeTbl> EmployeeTbls { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EmpDeptDb;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentTbl>(entity =>
        {
            entity.HasKey(e => e.DeptId);

            entity.ToTable("DepartmentTbl");

            entity.Property(e => e.DeptId).ValueGeneratedNever();
            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<EmployeeTbl>(entity =>
        {
            entity.HasKey(e => e.EmpId);

            entity.ToTable("EmployeeTbl");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
            entity.Property(e => e.EmpName)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Dept).WithMany(p => p.EmployeeTbls)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTbl_DepartmentTbl");

            entity.HasOne(d => d.Lib).WithMany(p => p.EmployeeTbls)
                .HasForeignKey(d => d.LibId)
                .HasConstraintName("FK_EmployeeTbl_Library1");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.LibId);

            entity.ToTable("Library");

            entity.Property(e => e.LibId).ValueGeneratedNever();
            entity.Property(e => e.LibName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
