using System;
using System.Collections.Generic;
using LPMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LPMS.Infrastructure.DbContexts;

public partial class LPMSDbContext : DbContext
{
    public LPMSDbContext()
    {
    }

    public LPMSDbContext(DbContextOptions<LPMSDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Endpoint> Endpoints { get; set; }

    public virtual DbSet<EndpointOperation> EndpointOperations { get; set; }

    public virtual DbSet<EndpointxSystemRole> EndpointxSystemRoles { get; set; }

    public virtual DbSet<Reference> References { get; set; }

    public virtual DbSet<ReferenceType> ReferenceTypes { get; set; }

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
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountID).HasName("PK_Account_AccountID");

            entity.ToTable("Account", "core");

            entity.Property(e => e.AccountID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityID).HasName("PK_City_CityID");

            entity.ToTable("City", "core");

            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name_EN).HasMaxLength(500);
            entity.Property(e => e.Name_MK).HasMaxLength(500);
            entity.Property(e => e.PostalCode).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_Country");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientID).HasName("PK_Client_ClientID");

            entity.ToTable("Client", "core");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Address2).HasMaxLength(500);
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Email2).HasMaxLength(256);
            entity.Property(e => e.IdDocumentNumber).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LegalName).HasMaxLength(500);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Phone2).HasMaxLength(50);
            entity.Property(e => e.TradeName).HasMaxLength(500);
            entity.Property(e => e.UniqueIdentificationNumber).HasMaxLength(13);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryID).HasName("PK_Country_CountryID");

            entity.ToTable("Country", "core");

            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name_EN).HasMaxLength(500);
            entity.Property(e => e.Name_MK).HasMaxLength(500);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentID).HasName("PK_Department_DeparmentID");

            entity.ToTable("Department", "core");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name_EN).HasMaxLength(256);
            entity.Property(e => e.Name_MK).HasMaxLength(256);

            entity.HasOne(d => d.Division).WithMany(p => p.Departments)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Department_Division");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.DivisionID).HasName("PK_Division_DivisionID");

            entity.ToTable("Division", "core");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name_EN).HasMaxLength(256);
            entity.Property(e => e.Name_MK).HasMaxLength(256);
        });

        modelBuilder.Entity<Endpoint>(entity =>
        {
            entity.HasKey(e => e.EndpointID).HasName("PK_Endpoint_EndpointID");

            entity.ToTable("Endpoint");

            entity.Property(e => e.Action).HasMaxLength(256);
            entity.Property(e => e.Controller).HasMaxLength(256);
            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FullPath).HasMaxLength(500);
            entity.Property(e => e.Method).HasMaxLength(10);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Route).HasMaxLength(256);
        });

        modelBuilder.Entity<EndpointOperation>(entity =>
        {
            entity.HasKey(e => e.EndpointOperationID).HasName("PK_EndpointOperation_EndpointOperationID");

            entity.ToTable("EndpointOperation");

            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Read).HasDefaultValue(true);

            entity.HasOne(d => d.Endpoint).WithMany(p => p.EndpointOperations)
                .HasForeignKey(d => d.EndpointId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EndpointOperation_Endpoint");
        });

        modelBuilder.Entity<EndpointxSystemRole>(entity =>
        {
            entity.HasKey(e => e.EndpointxSystemRoleID).HasName("PK_EndpointxSystemRole_EndpointxSystemRoleID");

            entity.ToTable("EndpointxSystemRole");

            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Endpoint).WithMany(p => p.EndpointxSystemRoles)
                .HasForeignKey(d => d.EndpointId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EndpointxSystemRole_Endpoint");
        });

        modelBuilder.Entity<Reference>(entity =>
        {
            entity.HasKey(e => e.ReferenceID).HasName("PK_Reference_ReferenceID");

            entity.ToTable("Reference", "core");

            entity.HasIndex(e => e.Code, "IX_Reference_Code");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description_EN).HasMaxLength(500);
            entity.Property(e => e.Description_MK).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name_EN).HasMaxLength(256);
            entity.Property(e => e.Name_MK).HasMaxLength(256);

            entity.HasOne(d => d.ReferenceType).WithMany(p => p.References)
                .HasForeignKey(d => d.ReferenceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reference_ReferenceType");
        });

        modelBuilder.Entity<ReferenceType>(entity =>
        {
            entity.HasKey(e => e.ReferenceTypeID).HasName("PK_ReferenceType_ReferenceID");

            entity.ToTable("ReferenceType", "core");

            entity.HasIndex(e => e.Code, "IX_ReferenceType_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description_EN).HasMaxLength(500);
            entity.Property(e => e.Description_MK).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedOn).HasPrecision(3);
            entity.Property(e => e.Name_EN).HasMaxLength(256);
            entity.Property(e => e.Name_MK).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
