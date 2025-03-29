using Domain.Entities;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider)
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

         
            await CreateUserAsync (userManager, "esuriel", "Admin123!", "Admin");
        }

        private static async Task CreateUserAsync(UserManager<ApplicationUser> userManager, string username, string password, string role)
        {
            if (await userManager.FindByNameAsync(username) == null)
            {
                var user = new ApplicationUser {Name = username, UserName = username, Email = $"{username}@mail.com", Pass = password ,Activo = true, FechaCreacion = System.DateTime.Now, Version = 1 };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
        public static void Initialize(ClinicContext context)
        {
            if (!context.Provincias.Any()) // Verifica si ya existen datos
            {
                var provincias = new List<Provincia>
            {
                new Provincia { Nombre = "Distrito Nacional", Poblacion = "1,029,110", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Azua", Poblacion = "214,311", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Bahoruco", Poblacion = "97,313", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Barahona", Poblacion = "187,105", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Dajabón", Poblacion = "78,723", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Duarte", Poblacion = "330,763", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Elías Piña", Poblacion = "63,029", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "El Seibo", Poblacion = "110,932", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Espaillat", Poblacion = "253,063", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Hato Mayor", Poblacion = "85,017", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Hermanas Mirabal", Poblacion = "94,285", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Independencia", Poblacion = "52,589", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "La Altagracia", Poblacion = "273,210", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "La Romana", Poblacion = "280,412", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "La Vega", Poblacion = "394,205", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "María Trinidad Sánchez", Poblacion = "140,925", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Monseñor Nouel", Poblacion = "206,808", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Monte Cristi", Poblacion = "109,607", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Monte Plata", Poblacion = "185,956", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 },
                new Provincia { Nombre = "Pedernales", Poblacion = "31,587", Activo = true, IdUsuarioCreacion = 1, FechaCreacion = DateTime.UtcNow, FechaModificacion = DateTime.UtcNow, IdUsuarioModificacion = 1, Version = 1 }
            };

                context.Provincias.AddRange(provincias);
                context.SaveChanges();
            }
        }
    }
}
