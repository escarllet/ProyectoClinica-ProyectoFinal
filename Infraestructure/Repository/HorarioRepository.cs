using Application.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTOs.Request.Horario;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infraestructure.Repository
{
    public class HorarioRepository : IHorario
    {
        private readonly ClinicContext _context;
        private readonly IEmployeeRepository _employee;
        public HorarioRepository(ClinicContext context, IEmployeeRepository employee)
        {
            _context = context;
            _employee = employee;
        }
  
        //Como médico interino/titular, quiero ver mi horario de consulta
        //para organizar mi agenda.
        
        public async Task<List<Horario>> ObtenerHorariosPorUsuarioAsync(int DoctorId)
        {
            var esSustituto = _context.DoctoresSustitutos.Where(c => c.Id == DoctorId && c.Activo).Any();
            if (esSustituto)
            {
                //esto es para saber a que doctorID esta sustituyendo actualmente
                var Sustituyendoa = _context.Sustituciones.FirstOrDefault(c => c.FechaDeBaja >= DateTime.Now && c.IdDoctorSustituto == DoctorId&& c.Activo);
                if (Sustituyendoa == null)
                {
                    throw new Exception("Usted No esta sustituyendo a ningun doctor, No hay horario para mostrar");
                }
                //id del doctor al que estoy sustituyendo
                int? idDoc = Sustituyendoa.DoctorInterinoId == null ? Sustituyendoa.DoctorTitularId : Sustituyendoa.DoctorInterinoId;
                return await _context.Horarios.Include(c => c.Doctor)
                .Where(h => h.Doctor.Id == idDoc && h.Activo && h.Doctor.Activo)
                .ToListAsync();
            }
            else
            {
                
                if (_context.Doctores.Where(c => c.Id == DoctorId).Any())
                {
                    return await _context.Horarios.Include(c => c.Doctor)
                    .Where(h => h.Doctor.Id == DoctorId && h.Activo && h.Doctor.Activo)
                    .ToListAsync();
                }
                throw new Exception("El empleado no es de tipo Doctor");
                
            }
            
        }
        public async Task AgregarHorarioAsync(HorarioDTO horario)
        {
            
            if (_context.DoctoresSustitutos.Where(C => C.Id == horario.IdDoctor).Any())
            {
                throw new Exception("El Doctor no puede ser sustituto");
            }
            if (_context.Doctores.FirstOrDefault(c => c.Id == horario.IdDoctor) == null)
            {
                throw new Exception("El Empleado tiene que ser Doctor");
            }
            if (ExisteHorarioAsync(horario.DiaSemana,horario.IdDoctor,horario.HoraInicio,horario.HoraFin).Result == false)
            {
                Horario horario1 = new Horario
                {
                    HoraInicio = horario.HoraInicio,
                    IdUsuarioCreacion = "N/A",//falta esto
                    HoraFin = horario.HoraFin,
                    DoctorId = horario.IdDoctor,
                    Version = 1,
                    DiaSemana = horario.DiaSemana,
                    FechaCreacion = DateTime.Now,
                    Activo = true
                    
                };
                
                _context.Horarios.Add(horario1);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("El horario a crear no se puede superponer con uno ya creado");
            }
           
        }

        private async Task<bool> ExisteHorarioAsync(string DiaSemana, int IdDoctor, TimeOnly HoraInicio, TimeOnly HoraFin)
        {
            return await _context.Horarios.AnyAsync(h =>
                h.DoctorId == IdDoctor && h.Activo &&
                h.DiaSemana == DiaSemana &&
                ((HoraInicio >= h.HoraInicio && HoraInicio < h.HoraFin) ||
                 (HoraFin > h.HoraInicio && HoraFin <= h.HoraFin) ||
                 (HoraInicio <= h.HoraInicio && HoraFin >= h.HoraFin)));
        }
        public async Task EditarHorarioAsync(UpdateHorarioDTO horario)
        {
            var HorarioActual = _context.Horarios.Find(horario.Id);
            if (HorarioActual == null)
            {
                throw new Exception("El horario que intenta actualizar no existe");
            }
           
            if (ExisteHorarioAsync(horario.DiaSemana, HorarioActual.DoctorId, horario.HoraInicio, horario.HoraFin).Result == false)
            {

                HorarioActual.HoraInicio = horario.HoraInicio;
                HorarioActual.IdUsuarioModificacion = "N/A";//falta esto
                HorarioActual.HoraFin = horario.HoraFin;
                HorarioActual.Version ++;
                HorarioActual.DiaSemana = horario.DiaSemana;
                HorarioActual.FechaModificacion = DateTime.Now;

               

                _context.Horarios.Update(HorarioActual);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("El horario a crear no se puede superponer con uno ya creado");
            }

        }
        public async Task EliminarHorarioAsync(int horario)
        {
            var HorarioActual = _context.Horarios.Find(horario);
            if (HorarioActual == null)
            {
                throw new Exception("El horario que intenta eliminar no existe");
            }

                HorarioActual.IdUsuarioModificacion = "N/A";//falta esto
                HorarioActual.Version ++;
                HorarioActual.FechaModificacion = DateTime.Now;
                HorarioActual.Activo = false;

                _context.Horarios.Update(HorarioActual);
                await _context.SaveChangesAsync();
            

        }
    }
}
