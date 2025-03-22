using Application.Contracts;
using Application.Services;
using Domain.Entities.Authentication;
using Infrastructure.Data;
using Infrastructure.DependencyInjection;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           // builder.Services.AddApplicationServices();
            // Add services to the container.
        
            builder.Services.AddDbContext<ClinicContext>(options =>
                options.UseSqlServer("Server=localhost;Database=clinicDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            );
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddControllers();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<ClinicContext>()
                 .AddDefaultTokenProviders();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
