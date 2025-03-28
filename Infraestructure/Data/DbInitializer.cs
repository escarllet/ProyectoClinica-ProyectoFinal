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
    }
}
