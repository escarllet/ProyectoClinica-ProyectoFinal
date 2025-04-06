using Application.Contracts;
using Application.DTOs.Request.Paciente;
using Application.DTOs.Response.Provincia;
using Domain.Entities;
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
        public async Task<bool> InsertarPaciente(InsertPacienteDTO pacienteDTO)
        {
            return await _paciente.InsertarPaciente(pacienteDTO);
        }
        public async Task<bool> EditarPaciente(PacienteDTO pacienteDTO)
        {
            return await _paciente.EditarPaciente(pacienteDTO);
        }
        public async Task<bool> EliminarPaciente(int paciente, string IdUserDoctor)
        {
            return await _paciente.EliminarPaciente(paciente, IdUserDoctor);
        }
        public async Task<List<GetPaciente>> VerMisPacientes(string DoctorId, string? filtro = null)
        {
            return await _paciente.VerMisPacientes(DoctorId,filtro);
        }
    }
}
