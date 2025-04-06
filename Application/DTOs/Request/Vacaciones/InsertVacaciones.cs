using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Vacaciones
{
    public class InsertVacaciones
    {
        public required DateTime FechaInicio { get; set; }

        public required DateTime FechaFinal { get; set; }
   

        public string? EmployeeUserId { get; set; }

    }
}
