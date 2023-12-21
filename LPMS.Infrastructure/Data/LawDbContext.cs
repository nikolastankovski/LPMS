using System;
using System.Collections.Generic;
using LPMS.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LPMS.Infrastructure.Data;

public partial class LawDbContext : DbContext
{
    public LawDbContext()
    {
    }

    public LawDbContext(DbContextOptions<LawDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //Get connection string from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.HasIndex(e => e.Code, "UC_Person_Code").IsUnique();

            entity.Property(e => e.ClientId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Client_ID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Address2).HasMaxLength(500);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasPrecision(3);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Email2).HasMaxLength(256);
            entity.Property(e => e.Forename).HasMaxLength(256);
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.IdDocumentNumber).HasMaxLength(10);
            entity.Property(e => e.IdDocumentTypeId).HasColumnName("IdDocumentTypeID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LegalName).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.NationalityId).HasColumnName("NationalityID");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Phone2).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(256);
            entity.Property(e => e.TradeName).HasMaxLength(500);
            entity.Property(e => e.UniqueIdentificationNumber).HasMaxLength(13);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.HasIndex(e => e.Code, "UC_Department_Code").IsUnique();

            entity.Property(e => e.DepartmentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Department_ID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasPrecision(3);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
