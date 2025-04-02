using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public required string Pass { get; set; }
        public bool Activo { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? IdUsuarioModificacion { get; set; }
        public int Version { get; set; }
    }
}
