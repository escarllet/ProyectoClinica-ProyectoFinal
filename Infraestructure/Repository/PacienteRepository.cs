using Application.Contracts;
using Application.DTOs.Request.Paciente;
using Domain.Entities;
using Domain.Entities.People;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<List<GetPaciente>> VerMisPacientes(string DoctorId, string? filtro = null) 
        {
            var doct = _context.Doctores.FirstOrDefault(c => c.UserId == DoctorId && c.Activo);
            if (doct == null)
            {
                throw new Exception("El Empleado no existe o no es de tipo Doctor, No puede insertar Pacientes");
            }
            var sus = _context.DoctoresSustitutos.FirstOrDefault(c => c.UserId == DoctorId);
            if (sus == null)
            {                
               
                var query = _context.Pacientes.Where(c=>c.Activo && c.IdDoctor == doct.Id).AsQueryable();

                if (!string.IsNullOrEmpty(filtro))
                {
                    query = query.Where(u => u.Name.Contains(filtro));
                }
                return await query.Select(c => new GetPaciente
                {
                    Id = c.Id,
                    Provincia = c.Provincia.Nombre,
                    CodigoPaciente = c.CodigoPaciente,
                    Telefono = c.Phone,
                    Direccion = c.Address,
                    CodigoPostal = c.PostalCode,
                    NombreCompleto = c.Name,
                    NIF = c.NIF,
                    NumeroSeguridadSocial = c.SocialSecurityNumber
                }).ToListAsync();

            }
            else if (sus.Activo) 
            {
               var idDoc = DoctorIsValid(sus.Id);
                var query = _context.Pacientes.Include(c=>c.Provincia).Where(c => c.Activo && c.IdDoctor == idDoc).AsQueryable();
                if (!string.IsNullOrEmpty(filtro))
                {
                    query = query.Where(u => u.Name.Contains(filtro)|| u.CodigoPaciente.Contains(filtro));
                }
                return await query.Select(c => new GetPaciente
                { 
                    Id = c.Id,
                    Provincia = c.Provincia.Nombre,
                    CodigoPaciente = c.CodigoPaciente,
                    Telefono = c.Phone,
                    Direccion = c.Address,
                    CodigoPostal = c.PostalCode,
                    NombreCompleto =c.Name,
                    NIF = c.NIF,
                    NumeroSeguridadSocial = c.SocialSecurityNumber
                }).ToListAsync();

            }
            throw new Exception("El doctor sustituto fue eliminado, no puede editar pacientes");
        }
        public async Task<bool> EliminarPaciente(int pacienteId, string IdUserDoctor)
        {
            var paciente = _context.Pacientes.FirstOrDefault(c => c.Id == pacienteId && c.Activo);
            if (paciente == null)
            {
                throw new Exception("El Paciente que intenta eliminar no existe");
            }

            paciente.IdUsuarioModificacion = IdUserDoctor;             
            paciente.Version++;
            paciente.FechaModificacion = DateTime.Now;
            paciente.Activo = false;

            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditarPaciente(PacienteDTO pacienteDTO)
        {
            IsValid(pacienteDTO.UserDoctorId, pacienteDTO.ProvinciaId);
            var paciente = _context.Pacientes.FirstOrDefault(c=> c.Id == pacienteDTO.Id && c.Activo);
            if (paciente == null)
            {
                throw new Exception("El Paciente que intenta editar no existe");
            }
            var sus = _context.DoctoresSustitutos.FirstOrDefault(c=>c.UserId == pacienteDTO.UserDoctorId);
            if (sus==null)
            {
                paciente.Name = pacienteDTO.NombreCompleto;
                paciente.CodigoPaciente = pacienteDTO.CodigoPaciente;
                paciente.Address = pacienteDTO.Direccion;
                paciente.Phone = pacienteDTO.Telefono;
                paciente.PostalCode = pacienteDTO.CodigoPostal;
                paciente.NIF = pacienteDTO.NIF;
                paciente.SocialSecurityNumber = pacienteDTO.NumeroSeguridadSocial;
                paciente.ProvinciaId = pacienteDTO.ProvinciaId;
                paciente.IdUsuarioModificacion = pacienteDTO.UserDoctorId;                   
                paciente.Version++;
                paciente.FechaModificacion = DateTime.Now;

                _context.Pacientes.Update(paciente);
                await _context.SaveChangesAsync();
                return true;

            }
            else if(sus.Activo)
            {
                var idDoc = DoctorIsValid(sus.Id);
                paciente.Name = pacienteDTO.NombreCompleto;
                paciente.CodigoPaciente = pacienteDTO.CodigoPaciente;
                paciente.Address = pacienteDTO.Direccion;
                paciente.Phone = pacienteDTO.Telefono;
                paciente.PostalCode = pacienteDTO.CodigoPostal;
                paciente.NIF = pacienteDTO.NIF;
                paciente.SocialSecurityNumber = pacienteDTO.NumeroSeguridadSocial;
                paciente.ProvinciaId = pacienteDTO.ProvinciaId;
                paciente.IdUsuarioModificacion = pacienteDTO.UserDoctorId;                    
                paciente.Version++;
                paciente.FechaModificacion = DateTime.Now;

                _context.Pacientes.Update(paciente);
                await _context.SaveChangesAsync();
                return true;

            }
            throw new Exception("El doctor sustituto fue eliminado, no puede editar pacientes");
     

        }
        private void IsValid(string DoctorId, int ProvinciaId)
        {
            if (_context.Doctores.Where(c=>c.UserId == DoctorId && c.Activo) == null)
            {
                throw new Exception("El Empleado no existe o no es de tipo Doctor, No puede insertar Pacientes");
            }
            if (_context.Provincias.Where(c => c.Id == ProvinciaId &&c.Activo) == null)
            {
                throw new Exception("La Provincia no Existe");
            }
        }
        private int? DoctorIsValid(int DoctorId)
        {
            var Sustituyendoa = _context.Sustituciones.FirstOrDefault(c => c.FechaDeBaja >= DateTime.Now && c.IdDoctorSustituto == DoctorId && c.Activo);
            if (Sustituyendoa == null)
            {
                throw new Exception("Usted No esta sustituyendo a ningun doctor, No puede realizar esta accion Pacientes");
            }
            //id del doctor al que estoy sustituyendo
            var idDoc = Sustituyendoa.DoctorInterinoId == null ? Sustituyendoa.DoctorTitularId : Sustituyendoa.DoctorInterinoId;
            if (idDoc == null)
            {
                throw new Exception("No se pudo obtener el doctor en la sustitucion, contactar a un Administrador.");
            }
            return idDoc;
        }
        public async Task<bool> InsertarPaciente(InsertPacienteDTO pacienteDTO)
        {

            IsValid(pacienteDTO.IdUserDoctor,pacienteDTO.ProvinciaId);
            var sus = _context.DoctoresSustitutos.FirstOrDefault(c => c.UserId == pacienteDTO.IdUserDoctor);
            if (sus == null)
            {
                var doctor = _context.Doctores.First(c =>c.Activo && c.UserId == pacienteDTO.IdUserDoctor);
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
                    IdUsuarioCreacion = pacienteDTO.IdUserDoctor,              
                    IdDoctor = doctor.Id,
                    Version = 1,                  
                    FechaCreacion = DateTime.Now,
                    Activo = true

                };

                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();
                return true;
            }
            else if(sus.Activo)
            {
                
                var idDoc = DoctorIsValid(sus.Id);
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
                    IdUsuarioCreacion = pacienteDTO.IdUserDoctor,                  
                    IdDoctor = idDoc.Value,
                    Version = 1,
                    FechaCreacion = DateTime.Now,
                    Activo = true

                };

                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();
                return true;
            }
            throw new Exception("El doctor sustituto fue eliminado, no puede editar pacientes");
        }

    }
}
