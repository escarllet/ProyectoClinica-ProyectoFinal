using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response.Employee
{
    public class DoctorDto
    {
        public required int DoctorId { get; set; }

        public required string DoctorNameEmail { get; set; }

    }
}
