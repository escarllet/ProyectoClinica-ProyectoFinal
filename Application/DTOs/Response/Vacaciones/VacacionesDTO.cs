using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response.Vacaciones
{
    public class VacacionesDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public DateTime FechaPlanificacion { get; set; }

        public string? AprobadaPor { get; set; }
        public string Estado { get; set; }

        public int EmployeeId { get; set; }
    }
}
