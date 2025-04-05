using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.Paciente
{
    public class InsertPacienteDTO
    {
        public int DoctorId { get; set; }
        public string? CodigoPaciente { get; set; }
        public string? Name { get; set; }
        public required string Address { get; set; }
        public string? Phone { get; set; }
        public int PostalCode { get; set; }
        public required string NIF { get; set; }
        public required string SocialSecurityNumber { get; set; }

        public int ProvinciaId { get; set; }

       

        
    }
}
