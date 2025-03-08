using System.Text.Json.Serialization;

namespace Domain.Entities.People
{
    public class DoctorSustituto : Doctor
    {
        //previene que sea serializado en json
        [JsonIgnore]
        public ICollection<Sustituciones>? Sustituciones { get; set; }
    }
}
