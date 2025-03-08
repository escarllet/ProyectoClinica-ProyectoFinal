using System.Text.Json.Serialization;

namespace Domain.Entities.People
{
    public class Person : Auditoria
    {
       
        public string? Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int PostalCode { get; set; }
        public string NIF { get; set; }
        public string SocialSecurityNumber { get; set; }

        public int IdProvincia { get; set; }

        public Provincia? Provincia { get; set; }
    }
}
