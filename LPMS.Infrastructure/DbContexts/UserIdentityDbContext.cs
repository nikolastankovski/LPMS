using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace LPMS.Infrastructure.Data
{
    public partial class UserIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public UserIdentityDbContext() { }
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options)
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

            builder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            builder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(x => x.Name).HasColumnName("Name_EN");
                entity.ToTable("ApplicationRole");
            });
            builder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRole");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("ApplicationUserClaim");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("ApplicationUserLogin");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("ApplicationUserToken");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("ApplicationRoleClaim");

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
