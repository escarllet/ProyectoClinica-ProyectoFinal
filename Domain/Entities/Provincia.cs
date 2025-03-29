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
        //falta agregar nombre de la provincia
        public required string Poblacion { get; set; }

        public required string Nombre { get; set; }

        //previene que sea serializado en json
        [JsonIgnore]
        public ICollection<Person>? People { get; set; }
    }
}
