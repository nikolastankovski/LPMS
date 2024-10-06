using LPMS.Domain.Models.ConfigModels;
using LPMS.Infrastructure.Data;
using LPMS.Infrastructure.DbContexts;
using LPMS.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LPMS.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPI(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .AddDbContext(builder)
            .AddJwtToken(builder)
            .AddSwagger();            

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<LPMSDbContext>((sp, options) =>
        {
            var auditingInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>()!;

            options
                .UseSqlServer(connectionString)
                .AddInterceptors(auditingInterceptor);
        });
        services.AddDbContext<SystemUserDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentity<SystemUser, SystemRole>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = true;

            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
        })
        .AddEntityFrameworkStores<SystemUserDbContext>()
        .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(2));

        return services;
    }

    public static IServiceCollection AddJwtToken(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services
            .AddOptions<JWTConfig>()
            .Bind(builder.Configuration.GetSection(JWTConfig.SectionName))
            .ValidateOnStart();

        JWTConfig jwtConfig = builder.Configuration.GetSection(JWTConfig.SectionName).Get<JWTConfig>() ?? new JWTConfig();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = jwtConfig.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.IssuerSigningKey)),
                    ValidateIssuer = jwtConfig.ValidateIssuer,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    ValidAudience = jwtConfig.ValidAudience,
                    ValidateAudience = jwtConfig.ValidateAudience,
                    RequireExpirationTime = jwtConfig.RequireExpirationTime,
                    ValidateLifetime = jwtConfig.ValidateLifetime,
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LPMS API", Version = "v1" });
            //c.EnableAnnotations();
            //c.SchemaFilter<CustomSchemaFilters>();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 123456abcdef'",
                Scheme = "Bearer",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement() 
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}