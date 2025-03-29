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
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddControllers();
            //builder.Services.AddDbContext<ClinicContext>(options =>
            //    options.UseSqlServer("Server=localhost;Database=clinicDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            //);
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //     .AddEntityFrameworkStores<ClinicContext>()
            //     .AddDefaultTokenProviders();
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //            ValidAudience = builder.Configuration["Jwt:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //        };
            //    });
            //

            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //builder.Services.AddScoped<EmployeeService>();
            //builder.Services.AddScoped<IAuthService, AuthRepository>();
            //builder.Services.AddScoped<AuthService>();
            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>();



            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ClinicContext>();
                context.Database.Migrate();
                await DbInitializer.Seed(services);
                DbInitializer.Initialize(context);
            }
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
