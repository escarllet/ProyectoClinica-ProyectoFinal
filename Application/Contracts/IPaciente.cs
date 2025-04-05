using Application.DTOs.Request.Paciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IPaciente
    {
        Task<bool> InsertarPaciente(InsertPacienteDTO pacienteDTO);
        Task<bool> EditarPaciente(PacienteDTO pacienteDTO);
        Task<bool> EliminarPaciente(int pacienteId);
        Task<List<GetPaciente>> VerMisPacientes(int DoctorId, string? filtro = null);
    }
}
