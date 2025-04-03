using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.User
{
    public class UsuarioPerfilDto
    {
        public int? Id { get; set; }
        public string? UserId { get; set; } 
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Rol { get; set; }

        // Información del empleado (si aplica)
        public string? CodigoEmpleado { get; set; }
        public DateTime? FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string? Direccion { get; set; }
        public int? CodigoPostal { get; set; }
        public string? NIF { get; set; }
        public string? SeguridadSocial { get; set; }
        public string? Provincia { get; set; }

        // Información específica de empleados especializados
        public string? NumeroColegiado { get; set; } // Solo para doctores
        public string? AreaOficina { get; set; } // Solo para administrativos
        public string? DescripcionZona { get; set; } // Solo para asistentes de zona
    }

}
