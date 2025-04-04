using Domain.Entities;
using Domain.Entities.Authentication;
using Domain.Entities.People;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider, ClinicContext context)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

          
            string[] roles = { "Admin", "DoctorSustituto", "DoctorInterino", "DoctorTitular",
            "AuxEnfermeria","ATS", "ATSZona","Celadores"};
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

           Initialize(context);
           await CreateUserAsync (userManager, "esuriel", "Admin123!", "Admin",context);

        }

        private static async Task CreateUserAsync(UserManager<ApplicationUser> userManager, string username, string password, string role, ClinicContext context)
        {
            if (await userManager.FindByNameAsync(username) == null)
            {
                var user = new ApplicationUser {Name = username, UserName = username, Email = $"{username}@mail.com", Pass = password ,Activo = true, FechaCreacion = DateTime.Now,IdUsuarioCreacion = "SYSTEM", Version = 1 };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                var a = userManager.FindByNameAsync(username).Result;
                await CreateEmployee(context, a.Id);
            }
        }
        private static async Task CreateEmployee(ClinicContext context, string userid)
        {
            if (!context.Administrativos.Any())
            {
                var provincia = context.Provincias.FirstOrDefault();
                var admi = new Administrativo
                {
                    Name = "Escarllet",
                    Address = "Mi casa",
                    UserId = userid,
                    Phone = "809-234-3020",
                    PostalCode = 11231,
                    NIF = "13213",
                    AreaOficina = "Sistemas De Informacion",
                    IdProvincia = provincia.Id,
                    fechaEntrada = DateTime.Now,
                    IdUsuarioCreacion = "SYSTEM",
                    SocialSecurityNumber = "124546",
                    CodigoEmpleado = "4433657",
                    Activo = true,
                    Version = 1,
                    FechaCreacion = DateTime.Now,
                };
               await context.Administrativos.AddAsync(admi);
               await context.SaveChangesAsync();
            }
        }
        private static void Initialize(ClinicContext context)
        {
            if (!context.Provincias.Any()) // Verifica si ya existen datos
            {
                var provincias = new List<Provincia>
                {
                    new Provincia { Nombre = "Distrito Nacional", Poblacion = "1,029,110", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Azua", Poblacion = "214,311", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now,  Version = 1 },
                    new Provincia { Nombre = "Bahoruco", Poblacion = "97,313", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now,  Version = 1 },
                    new Provincia { Nombre = "Barahona", Poblacion = "187,105", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Dajabón", Poblacion = "78,723", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Duarte", Poblacion = "330,763", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now,  Version = 1 },
                    new Provincia { Nombre = "Elías Piña", Poblacion = "63,029", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now,  Version = 1 },
                    new Provincia { Nombre = "El Seibo", Poblacion = "110,932", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Espaillat", Poblacion = "253,063", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Hato Mayor", Poblacion = "85,017", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Hermanas Mirabal", Poblacion = "94,285", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now,  Version = 1 },
                    new Provincia { Nombre = "Independencia", Poblacion = "52,589", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "La Altagracia", Poblacion = "273,210", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now,  Version = 1 },
                    new Provincia { Nombre = "La Romana", Poblacion = "280,412", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "La Vega", Poblacion = "394,205", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now,  Version = 1 },
                    new Provincia { Nombre = "María Trinidad Sánchez", Poblacion = "140,925", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Monseñor Nouel", Poblacion = "206,808", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now,  Version = 1 },
                    new Provincia { Nombre = "Monte Cristi", Poblacion = "109,607", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Monte Plata", Poblacion = "185,956", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 },
                    new Provincia { Nombre = "Pedernales", Poblacion = "31,587", Activo = true, IdUsuarioCreacion = "SYSTEM", FechaCreacion = DateTime.Now, Version = 1 }
                };

                context.Provincias.AddRange(provincias);
                context.SaveChanges();
            }
            
                

        }
    }
}
