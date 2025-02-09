using Microsoft.EntityFrameworkCore;
using CustomerBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace CustomerBackend.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies", "dbo");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("company_id")
                    .UseIdentityColumn();

                entity.Property(e => e.CompanyName)
                    .HasColumnName("company_name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.TaxId)
                    .HasColumnName("tax_id")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(200);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValue(true)
                    .IsRequired();

                // Agregar restricción única para tax_id
                entity.HasIndex(e => e.TaxId)
                    .HasDatabaseName("UK_companies_tax_id")
                    .IsUnique();

                // Agregar índices
                entity.HasIndex(e => e.TaxId)
                    .HasDatabaseName("idx_companies_tax_id");

                entity.HasIndex(e => e.CompanyName)
                    .HasDatabaseName("idx_companies_company_name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers", "dbo");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("bigint")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20);

                entity.Property(e => e.Address)
                    .HasMaxLength(200);

                entity.Property(e => e.CompanyId)
                    .IsRequired();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}