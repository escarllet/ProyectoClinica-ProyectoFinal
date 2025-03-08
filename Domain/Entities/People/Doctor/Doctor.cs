using System.Text.Json.Serialization;

namespace Domain.Entities.People
{
    public class Doctor : Employee
    {
        //numero execuartur
        public required string NumeroColegiado { get; set; }

        //previene que sea serializado en json
        [JsonIgnore]
        public ICollection<Paciente>? Paciente { get; set; } 
        
        [JsonIgnore]
        public ICollection<Horario>? Horario { get; set; }
    }
}
