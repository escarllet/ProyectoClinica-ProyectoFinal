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

        //previene que sea serializado en json
        [JsonIgnore]
        public ICollection<Vacaciones>? Vacaciones { get; set; }
    }
}
