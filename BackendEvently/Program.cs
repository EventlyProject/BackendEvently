
using BackendEvently.Data;
using BackendEvently.Service;
using Evently.Shared.Mapping;
using Evently.Shared.Service;
using Evently.Shared.Service.InterfaceService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BackendEvently
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add User Secrets (for sensitive config in development)
            builder.Configuration.AddUserSecrets<Program>();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Register application services for dependency injection
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ICategoryService , CategoryService>();
            builder.Services.AddScoped<IParticipantService,ParticipantService>();
            builder.Services.AddScoped<IUserService , UserService>();
            builder.Services.AddScoped<IJwtService , JwtService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IAdminService , AdminService>();

            // Register the database context with SQL Server provider
            // Remember to check and update your connection string in appsettings.json
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure JWT authentication
            var Key = builder.Configuration["Jwt:Key"];
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key!))
                    };
                });
            // Configure Swagger to use JWT Bearer authentication
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendEvently", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
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
            new string[] {}
        }
    });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();// Enable Swagger in development
                app.UseSwaggerUI();// Enable Swagger UI in development
            }

            app.UseHttpsRedirection();// Redirect HTTP requests to HTTPS

            // Enable Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();// Map controller routes

            app.Run();// Start the application
        }
    }
}