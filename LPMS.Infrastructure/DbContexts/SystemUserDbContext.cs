using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace LPMS.Infrastructure.Data
{
    public partial class SystemUserDbContext : IdentityDbContext<SystemUser, SystemRole, Guid, IdentityUserClaim<Guid>, SystemUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public SystemUserDbContext() { }
        public SystemUserDbContext(DbContextOptions<SystemUserDbContext> options)
            : base(options)
        {
        }
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");

            builder.Entity<SystemUser>(entity =>
            {
                entity.ToTable(nameof(SystemUser));
                entity.Property(x => x.PasswordChangePeriodInMonths).HasDefaultValueSql("12");
                entity.Property(x => x.LastLogin).HasDefaultValueSql("GETDATE()");
                entity.Property(x => x.LastPasswordChange).HasDefaultValueSql("GETDATE()");
            });
            builder.Entity<SystemRole>(entity =>
            {
                entity.ToTable(nameof(SystemRole));
                entity.Property(x => x.CreatedOnUTC).HasDefaultValueSql("GETDATE()");
                entity.Property(x => x.IsActive).HasDefaultValueSql("1");
            });
            builder.Entity<SystemUserRole>(entity =>
            {
                entity.ToTable(nameof(SystemUserRole));
                entity.Property(x => x.CreatedOn).HasDefaultValueSql("GETDATE()");
            });
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("SystemUserClaim");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("SystemUserLogin");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("SystemUserToken");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("SystemRoleClaim");

            OnModelCreatingPartial(builder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            builder.Properties<DateOnly?>()
                .HaveConversion<NullableDateOnlyConverter>()
                .HaveColumnType("date");
        }
        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            /// <summary>
            /// Creates a new instance of this converter.
            /// </summary>
            public DateOnlyConverter() : base(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    d => DateOnly.FromDateTime(d))
            { }
        }

        /// <summary>
        /// Converts <see cref="DateOnly?" /> to <see cref="DateTime?"/> and vice versa.
        /// </summary>
        public class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
        {
            /// <summary>
            /// Creates a new instance of this converter.
            /// </summary>
            public NullableDateOnlyConverter() : base(
                d => d == null
                    ? null
                    : new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)),
                d => d == null
                    ? null
                    : new DateOnly?(DateOnly.FromDateTime(d.Value)))
            { }
        }
    }
}
