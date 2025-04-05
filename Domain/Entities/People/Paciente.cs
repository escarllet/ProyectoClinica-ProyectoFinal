using System.Text.Json.Serialization;

namespace Domain.Entities.People
{
    public class Paciente : Person
    {
        public string? CodigoPaciente { get; set; }

        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
