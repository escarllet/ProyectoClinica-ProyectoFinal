using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Autentication
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
