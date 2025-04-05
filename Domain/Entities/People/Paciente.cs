using System.Text.Json.Serialization;

namespace Domain.Entities.People
{
    public class Paciente : Person
    {
        public string? CodigoPaciente { get; set; }

        public int IdDoctor { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
