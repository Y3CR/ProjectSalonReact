using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectSalonReact.Models;

public partial class ProjectSalonContext : DbContext
{
    public ProjectSalonContext()
    {
    }

    public ProjectSalonContext(DbContextOptions<ProjectSalonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Y3CR\\SQLEXPRESS; DataBase=ProjectSalon;Integrated Security=true; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK_products");

            entity.ToTable("product");

            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.DateAdmission)
                .HasColumnType("datetime")
                .HasColumnName("dateAdmission");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ImagePath)
                .IsUnicode(false)
                .HasColumnName("imagePath");
            entity.Property(e => e.NameProduct)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nameProduct");
            entity.Property(e => e.Stocks).HasColumnName("stocks");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Lastname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
