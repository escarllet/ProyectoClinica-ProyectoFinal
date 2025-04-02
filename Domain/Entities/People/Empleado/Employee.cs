using Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.People
{
    public class Employee : Person
    {
        public string? CodigoEmpleado { get; set; }

        public DateTime? fechaEntrada { get; set; }

        public DateTime? fechaSalida { get; set; }

        public string UserId { get; set; } = null!;

        //previene que sea serializado en json
        [JsonIgnore]
        public ApplicationUser User { get; set; }

        [JsonIgnore]
        public ICollection<Vacaciones>? Vacaciones { get; set; }
    }
}
