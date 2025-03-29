using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response.Employee
{
    public class ObtenerSustituciones
    {

        public required int MedicoTitularId { get; set; }
        public required int MedicoSustitutoId { get; set; }

        public required DateTime FechaInicio { get; set; }

        public required DateTime FechaFin { get; set; }
    }
}
