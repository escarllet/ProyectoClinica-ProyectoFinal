
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.People
{
    public class Administrativo : Employee
    {
        public string? AreaOficina { get; set; }
    }
}
