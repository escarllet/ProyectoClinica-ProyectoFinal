using Domain.Entities.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Provincia : Auditoria
    {
        public string Poblacion { get; set; }

        //previene que sea serializado en json
        [JsonIgnore]
        public ICollection<Person>? People { get; set; }
    }
}
