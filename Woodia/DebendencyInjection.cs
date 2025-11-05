using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

public static class DebendencyInjection
{
    public static IServiceCollection AddDependecy(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddFSwaggerDocs();
        services.AddControllers();
        services.AddAuthConfig(configuration);
        services.AddMapsterConfig();
        services.AddScoped<IAuth, AuthService>();
        services.AddSingleton<IjwtProvider, JwtProvider>();
        services.AddScoped<IEmailSender, EmailService>();
        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
        services.Configure<SmtpSetting>(configuration.GetSection(nameof(SmtpSetting)));

        var connectionstring = configuration.GetConnectionString("key");

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionstring));

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();
        return services;
    }
    static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }
    public static IServiceCollection AddFSwaggerDocs(this IServiceCollection services)
    {
        services
           .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("admin", new OpenApiInfo
                {
                    Title = "Admin API",
                    Version = "v1",
                    Description = "API endpoints for administrators only"
                });

                // User API Documentation  
                options.SwaggerDoc("user", new OpenApiInfo
                {
                    Title = "User API",
                    Version = "v1",
                    Description = "API endpoints for regular users"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Add Token"
                });

                // Global Security Requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type =ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] {}
                }
            });

                // XML Comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        return services;
    }

    public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        // services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        var JwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings?.key!)),
                ValidIssuer = JwtSettings?.Issuer,
                ValidAudience = JwtSettings?.Audience
            };


        });

        services.Configure<IdentityOptions>(o =>
        {
            o.Password.RequiredLength = 8;
            o.SignIn.RequireConfirmedEmail = true;
            o.User.RequireUniqueEmail = true;
        });



        return services;
    }

}