using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response.Employee
{
    public class ObtenerSustituciones
    {

        public int? DoctorId { get; set; }
        public int? DoctorSustitutoId { get; set; }

        public required DateTime FechaInicio { get; set; }

        public required DateTime FechaFin { get; set; }
    }
}
