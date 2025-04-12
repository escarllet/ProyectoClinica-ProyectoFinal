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

        public string? NombreEmployee { get; set; }
        public string? EmailEmployee { get; set; }

        public string? Role { get; set; }
        public required DateTime FechaInicio { get; set; }

        public required DateTime FechaFinal { get; set; }

        public DateTime? FechaPlanificacion { get; set; }

        public string? AprobadaPor { get; set; }
        public string Estado { get; set; }
      
        public int? EmployeeId { get; set; }
    }
}
