using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Employee
{
    public class UpdateSustitucionDto
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DoctorSustitutoId { get; set; }
        public int DoctorId { get; set; }
    }
}
