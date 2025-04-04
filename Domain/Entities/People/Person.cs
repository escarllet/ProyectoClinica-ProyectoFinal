using System.Text.Json.Serialization;

namespace Domain.Entities.People
{
    public class Person : Auditoria
    {
       
        public string? Name { get; set; }
        public required string Address { get; set; }
        public string? Phone { get; set; }
        public int PostalCode { get; set; }
        public required string NIF { get; set; }
        public required string SocialSecurityNumber { get; set; }

        public int ProvinciaId { get; set; }

        public Provincia? Provincia { get; set; }
    }
}
