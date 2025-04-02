using Domain.Entities.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vacaciones : Auditoria
    {
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public DateTime FechaPlanificacion { get; set; }

        public string? AprobadaPor { get; set; }
        public string Estado { get; set; }

        public int IdEmployee { get; set; }

        public Employee? Employee { get; set; }
    }
}
