using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Employee
{
    public class UpdateSustitucionDto
    {
        public required int Id { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }
        public required int DoctorSustitutoId { get; set; }
        public required int DoctorId { get; set; } 
        public string? ModifyUserId { get; set; }
    }
}
