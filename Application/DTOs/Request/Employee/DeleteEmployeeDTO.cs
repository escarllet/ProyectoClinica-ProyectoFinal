using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Employee
{
    public class DeleteEmployeeDTO
    {
        public required int Id { get; set; }

        public required DateTime FechaSalida { get; set; }

        public string? IdUserEmployee { get; set; }
    }
}
