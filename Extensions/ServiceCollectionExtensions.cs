using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Book_Keep.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ScopeService(this IServiceCollection service)
        {
            // Helpers
            service.AddScoped<ExcelHelper>();
            service.AddScoped<TimeHelper>();
            service.AddScoped<AuthenticationHelper>();
            // Service
            service.AddScoped<BookService>();
            // Queries
            service.AddScoped<BookQueries>();
            return service;
        }
        // Swagger Documentation
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection service)
        {
            service.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new() { Title = "Book Keep API", Version = "v1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[]{}
                    }
                });
            });
            return service;
        }
        // JWT Authentication
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Headers["Authorization"].ToString();
                            if (!string.IsNullOrEmpty(accessToken) && !accessToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            return service;
        }
        // CORS
        public static IServiceCollection AddCustomCORS(this IServiceCollection service)
        {
            service.AddCors(o =>
            {
                o.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            return service;
        }
    }
}
