using Application.Contracts;
using Application.DTOs.Request.Paciente;
using Domain.Entities;
using Domain.Entities.People;
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
        public async Task<bool> InsertarPaciente(InsertPacienteDTO pacienteDTO)
        {
           
            if (_context.Doctores.Find(pacienteDTO.DoctorId) == null)
            {
                throw new Exception("Usted No es Doctor, No puede insertar Pacientes");
            }
            if (_context.Provincias.Find(pacienteDTO.ProvinciaId)==null)
            {
                throw new Exception("La Provincia no Existe");
            }
            var sus = _context.DoctoresSustitutos.Find(pacienteDTO.DoctorId);
            if (sus == null)
            {
                Paciente paciente = new Paciente
                {
                    Name = pacienteDTO.NombreCompleto,
                    CodigoPaciente = pacienteDTO.CodigoPaciente,
                    Address = pacienteDTO.Direccion,
                    Phone = pacienteDTO.Telefono,
                    PostalCode = pacienteDTO.CodigoPostal,
                    NIF = pacienteDTO.NIF,
                    SocialSecurityNumber = pacienteDTO.NumeroSeguridadSocial,
                    ProvinciaId = pacienteDTO.ProvinciaId,
                    IdUsuarioCreacion = "N/A",//falta esto                    
                    IdDoctor = pacienteDTO.DoctorId,
                    Version = 1,                  
                    FechaCreacion = DateTime.Now,
                    Activo = true

                };

                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();
            }
            else
            {
                var Sustituyendoa = _context.Sustituciones.FirstOrDefault(c => c.FechaDeBaja >= DateTime.Now && c.IdDoctorSustituto == pacienteDTO.DoctorId && c.Activo);
                if (Sustituyendoa == null)
                {
                    throw new Exception("Usted No esta sustituyendo a ningun doctor, No puede insertar Pacientes");
                }
                //id del doctor al que estoy sustituyendo
               var idDoc = Sustituyendoa.DoctorInterinoId == null ? Sustituyendoa.DoctorTitularId : Sustituyendoa.DoctorInterinoId;
                if (idDoc == null)
                {
                    throw new Exception("No se pudo obtener el doctor en la sustitucion, contactar a un Administrador.");
                }
                Paciente paciente = new Paciente
                {
                    Name = pacienteDTO.NombreCompleto,
                    CodigoPaciente = pacienteDTO.CodigoPaciente,
                    Address = pacienteDTO.Direccion,
                    Phone = pacienteDTO.Telefono,
                    PostalCode = pacienteDTO.CodigoPostal,
                    NIF = pacienteDTO.NIF,
                    SocialSecurityNumber = pacienteDTO.NumeroSeguridadSocial,
                    ProvinciaId = pacienteDTO.ProvinciaId,
                    IdUsuarioCreacion = "N/A",//falta esto                    
                    IdDoctor = idDoc.Value,
                    Version = 1,
                    FechaCreacion = DateTime.Now,
                    Activo = true

                };

                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();
            }
            return true;
        }

    }
}
