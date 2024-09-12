using Microsoft.Extensions.Configuration;

namespace LPMS.Infrastructure.DbContexts;

public partial class LPMSViewsDbContext : DbContext
{
    public LPMSViewsDbContext()
    {
    }

    public LPMSViewsDbContext(DbContextOptions<LPMSViewsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<vwApplicationUser> vwApplicationUsers { get; set; }

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
        modelBuilder.Entity<vwApplicationUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwApplicationUser", "core");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Role).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
