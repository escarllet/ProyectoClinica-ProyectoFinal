using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Paciente
{
    public class GetPaciente
    {
        public int Id { get; set; }
        public string? CodigoPaciente { get; set; }
        public string? NombreCompleto { get; set; }
        public required string Direccion { get; set; }
        public string? Telefono { get; set; }
        public int CodigoPostal { get; set; }
        public required string NIF { get; set; }
        public required string NumeroSeguridadSocial { get; set; }

        public required string Provincia { get; set; }
    }
}
