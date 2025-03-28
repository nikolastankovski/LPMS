using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using LPMS.API;
using LPMS.Application;
using LPMS.Infrastructure;
using LPMS.Infrastructure.Data;
using LPMS.Infrastructure.DbContexts;
using LPMS.Domain.Models.ConfigModels;
using LPMS.API.Middleware;
using LPMS.Infrastructure.Interceptors;

var builder = WebApplication.CreateBuilder(args);

#region DBCONTEXT & IDENTITY
builder.Services.AddTransient<UpdateAuditableEntitiesInterceptor>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LPMSDbContext>((sp, options) =>
{
    var auditingInterceptor = sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>()!;
   
    options
        .UseNpgsql(connectionString)
        //.UseSqlServer(connectionString)
        .AddInterceptors(auditingInterceptor);
});

//builder.Services.AddDbContext<SystemUserDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<SystemUserDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddIdentity<SystemUser, SystemRole>(options =>
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

builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(2));
#endregion

#region JWT
JWTConfig jwtConfig = builder.Configuration.GetSection(JWTConfig.SectionName).Get<JWTConfig>() ?? new JWTConfig();
builder.Services
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
#endregion

#region CUSTOM SETTINGS
builder.Services
            .AddOptions<JWTConfig>()
            .Bind(builder.Configuration.GetSection(JWTConfig.SectionName))
            .ValidateOnStart();

builder.Services
        .AddApplication()
        .AddInfrastructure()
        .AddAPI();

//builder.AddEmailService();
#endregion

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();

#region SWAGGER
builder.Services.AddSwaggerGen(c =>
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
        }
    );
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.DefaultModelsExpandDepth(-1);
    });
    await app.ApplyMigrationsAsync();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CultureValidationMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.RegisterEndpoints();



app.Run();