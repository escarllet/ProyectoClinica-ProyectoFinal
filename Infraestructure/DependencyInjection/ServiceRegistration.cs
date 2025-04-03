using Application.Contracts;
using Application.Services;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;

using Infraestructure.Repository;

namespace Infraestructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<ClinicContext>(options =>
              options.UseSqlServer("Server=localhost;Database=clinicDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
          );
            services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<ClinicContext>()
                 .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aG1zZGdmMDk4MjNhc2Rma2prbGg0NTY3OGZnZGg=")),
                    };
                });
            services.AddHttpContextAccessor();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<EmployeeService>();
            services.AddScoped<IAuthService, AuthRepository>();
            services.AddScoped<IProvinciaRepository, ProvinciaRepository>();
            services.AddScoped<IMedicoSustitucionService, SustitucionesRepository>();
            services.AddScoped<IVacacionesRepository, VacacionesRepository>();
            services.AddScoped<IHorario, HorarioRepository>();
            services.AddScoped<HorarioService>();
            services.AddScoped<MedicoSustitucionService>();
            services.AddScoped<VacacionesServices>();
            services.AddScoped<AuthService>();
            services.AddScoped<ProvinciaService>();
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();


        }
    }
}
