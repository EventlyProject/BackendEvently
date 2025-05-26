
using BackendEvently.Service;
using Evently.Shared.Mapping;
using Evently.Shared.Service;
using Evently.Shared.Service.InterfaceService;

namespace BackendEvently
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add User Secrets (MUST be before we access configuration)
            builder.Configuration.AddUserSecrets<Program>();

            // Add services to the container.


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ICategoryService , CategoryService>();
            builder.Services.AddScoped<IParticipantService,ParticipantService>();
            builder.Services.AddScoped<IUserService , UserService>();
            builder.Services.AddScoped<IJwtService , JwtService>();

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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Enable Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
