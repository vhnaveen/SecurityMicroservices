using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SecurityMicroservice_A.DatabaseModel.SecurityDB.Models;

namespace SecurityMicroservice_A.DatabaseModel.SecurityDB.DBContext
{
    public partial class TcpSecurityDbContext : DbContext
    {
        public TcpSecurityDbContext()
        {
        }

        public TcpSecurityDbContext(DbContextOptions<TcpSecurityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Policy> Policies { get; set; } = null!;
        public virtual DbSet<Scope> Scopes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=SecurityDatabase");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Scope>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.Scopes)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK__Scopes__PolicyId__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
