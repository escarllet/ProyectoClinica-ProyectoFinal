using Domain.Entities.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sustituciones : Auditoria
    {
        public DateTime FechaDeBaja { get; set; }

        public DateTime FechadDeAlta { get; set; }

        public int IdDoctorSustituto { get; set; }


        public DoctorSustituto? DoctorSustituto { get; set; }
    }
}
