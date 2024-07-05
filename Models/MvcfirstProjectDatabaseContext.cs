using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PracticeMVCProject.Models;

public partial class MvcfirstProjectDatabaseContext : DbContext
{
    public MvcfirstProjectDatabaseContext()
    {
    }

    public MvcfirstProjectDatabaseContext(DbContextOptions<MvcfirstProjectDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=Conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserList__CB9A1CFF04D529C0");

            entity.ToTable("UserList");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .HasColumnName("userPassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
