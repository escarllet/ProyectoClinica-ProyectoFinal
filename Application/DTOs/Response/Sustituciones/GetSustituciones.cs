using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response.Sustituciones
{
    public class GetSustituciones
    {
        public required int Id { get; set; }
        public required string DoctorName { get; set; }

        public required string DoctorType { get; set; }
        public required string DoctorSustitutoName { get; set; }

        public required string EstaActiva { get; set; }

        public required DateTime FechaInicio { get; set; }

        public required DateTime FechaFin { get; set; }
    }
}
