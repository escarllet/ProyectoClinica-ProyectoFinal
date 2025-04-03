using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response.Provincia
{
    public class ProvinciaDTO
    {
        public required int Id { get; set; }
        public required string Poblacion { get; set; }

        public required string Nombre { get; set; }

    }
}
