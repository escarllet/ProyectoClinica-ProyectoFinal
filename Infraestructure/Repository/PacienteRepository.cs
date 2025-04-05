using Application.Contracts;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class PacienteRepository :IPaciente
    {
        private readonly ClinicContext _context;
        public PacienteRepository(ClinicContext context)
        {
            _context = context;

        }
        //get all pacientes by iddoctor, tener en cuenta que si el doctor es sustituto,
        //el vera los pacientes del doctor al que esta sustituyendo
        //
        //registrar paciente, si el doctor es sustituto, el paciente se asignara
        // al doctor que esta sustituyendo
        //
        //Editar paciente, si el doctor es sustituto, el paciente se le actualizara
        // al doctor que esta sustituyendo
        //
        //eliminar paciente, el doctor sustituto no podra hacer esto
        //
        //
        public void InsertarPaciente()
        {

        }

    }
}
