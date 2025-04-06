using Infraestructure.DependencyInjection;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {

        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
        
         
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", policy =>
                {
                    policy.AllowAnyOrigin()      
                          .AllowAnyMethod()      
                          .AllowAnyHeader();     
                });
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddControllers();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ClinicContext>();
                context.Database.Migrate();
                await DbInitializer.Seed(services,context);
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseRouting();              // <- ?? SIEMPRE antes de auth
            app.UseCors("AllowAnyOrigin");
            app.UseAuthentication();       // <- ?? OBLIGATORIO
            app.UseAuthorization();        // <- ?? Despu?s de auth
            app.MapControllers();          // <- ?? Finalmente, mapear endpoints

            app.Run();
        }
    }
}
