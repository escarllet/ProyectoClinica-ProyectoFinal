using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Pass { get; set; }
        public bool Activo { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public int Version { get; set; }
    }
}
