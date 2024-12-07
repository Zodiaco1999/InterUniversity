using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UniversityApi.Common.JWT;
using UniversityApi.DataAccess.Context;

namespace UniversityApi;

public static class ContenedorDependencias
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Adición del patron mediador
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        // Adición Contexto de base de datos
        services.AddDbContext<UniversidadContext>(options => options.UseSqlServer(configuration.GetConnectionString("UniversidadConnStr")));
        // Adición y configuracion de Swagger
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Por favor inserte el jwt despues de la palabra bearer de esta forma \"<strong>bearer {JWT}</strong>\"",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        // Adición de AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // Añadir autentificación el Aplicación por JWT
        var jwtOptions = configuration.GetSection(nameof(JWTOptions));

        services.Configure<JWTOptions>(options =>
        {
            options.Issuer = jwtOptions[nameof(JWTOptions.Issuer)]!;
            options.Audience = jwtOptions[nameof(JWTOptions.Audience)]!;
            options.ValidForMinutes = int.Parse(jwtOptions[nameof(JWTOptions.ValidForMinutes)]!);
            options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["SecretKeyJWT"]!)), SecurityAlgorithms.Aes128CbcHmacSha256);
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions[nameof(JWTOptions.Issuer)],
            
                    ValidateAudience = true,
                    ValidAudience = jwtOptions[nameof(JWTOptions.Audience)],
            
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
            
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyJWT"]!)),
            
                    ClockSkew = TimeSpan.FromMinutes(configuration.GetValue<int>("JwtOptions:ValidForMinutes"))
                };
            });

        services.AddScoped<IJWTFactory, JWTFactory>();

        return services;
    }
}
