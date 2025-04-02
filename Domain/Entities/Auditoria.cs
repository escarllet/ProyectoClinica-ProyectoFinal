using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Auditoria
    {
        public int Id { get; set; }
        public bool Activo { get; set; }
        public required string IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? IdUsuarioModificacion { get; set; }
        public int Version { get; set; }
    }
}
