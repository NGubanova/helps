using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.Models;

public partial class LoginContext : DbContext
{
    public LoginContext()
    {
        //!!!!!!!!!!!!!!
        Database.EnsureCreated();
    }

    public LoginContext(DbContextOptions<LoginContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        //!!!!!!!!!!!!!!!!!!!!!!!
        optionsBuilder.UseSqlServer("Server=Natikian;Database=Login;Trusted_Connection=True;Encrypt=False");

        //Scaffold - DbContext "Server=localhost;Database=login;Trusted_Connection=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3213E83F4896E366");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("author");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F9C1A8A3A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("role");

            entity.HasMany(d => d.Books).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserBook",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserBooks__bookI__49C3F6B7"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserBooks__userI__48CFD27E"),
                    j =>
                    {
                        j.HasKey("UserId", "BookId").HasName("PK__UserBook__032446EF8BB97565");
                        j.ToTable("UserBooks");
                        j.IndexerProperty<int>("UserId").HasColumnName("userId");
                        j.IndexerProperty<int>("BookId").HasColumnName("bookId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
