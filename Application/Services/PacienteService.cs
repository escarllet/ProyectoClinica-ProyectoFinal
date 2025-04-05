using Application.Contracts;
using Domain.Entities.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PacienteService
    {
        private readonly IPaciente _paciente;
        public PacienteService(IPaciente paciente)
        {
            _paciente = paciente;
        }

    }
}
