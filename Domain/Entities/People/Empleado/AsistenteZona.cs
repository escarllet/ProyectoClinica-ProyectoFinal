
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.People
{
    public class AsistenteZona : Asistente
    {
        public required string DescripcionZona { get; set; }
    }
}
