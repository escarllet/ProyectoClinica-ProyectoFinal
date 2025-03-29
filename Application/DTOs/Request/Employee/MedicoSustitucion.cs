using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Employee
{
    public class MedicoSustitucion
    {
        public required string MedicoTitularId { get; set; }
        public required string MedicoSustitutoId { get; set; }

        public required DateTime FechaInicio { get; set; }

        public required DateTime FechaFin { get; set; }
    }
}
