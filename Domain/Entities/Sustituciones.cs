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
        //falta agregar ID del titular
        public DateTime FechaDeBaja { get; set; }

        public DateTime FechadDeAlta { get; set; }

        public int? IdDoctorSustituto { get; set; }

        public int? DoctorTitularId { get; set; }
        public int? DoctorInterinoId { get; set; }

        public DoctorSustituto? DoctorSustituto { get; set; }
        public DoctorTitular? DoctorTitular { get; set; }
        public DoctorInterino? DoctorInterino { get; set; }
    }
}
