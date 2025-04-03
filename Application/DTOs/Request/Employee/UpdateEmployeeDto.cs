using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Employee
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string CodigoEmpleado { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public required string Direccion { get; set; }
        public string? Telefono { get; set; }
        public int CodigoPostal { get; set; }
        public required string NIF { get; set; }
        public string? NumeroColegiado { get; set; }
        public string? DescripcionZona { get; set; }
        public string? AreaOficina { get; set; }
        public DateTime FechaEntradaEmpleado { get; set; }
        public required string NumeroSeguridadSocial { get; set; }

        public int IdProvincia { get; set; }
    }
}
