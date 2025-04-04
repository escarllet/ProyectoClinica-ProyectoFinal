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
        //estoy trabajando en esto ahora 0.0
        //Como médico interino/titular, quiero ver mi horario de consulta
        //para organizar mi agenda.
        //falta integrar con los sustitutos
        public async Task<List<Horario>> ObtenerHorariosPorUsuarioAsync(string idUsuario)
        {
            //falla integrar las sustituciones
            return await _context.Horarios.Include(c=> c.Doctor)
                .Where(h => h.Doctor.UserId == idUsuario)
                .ToListAsync();
        }
        public async Task AgregarHorarioAsync(HorarioDTO horario)
        {
            var IdEmployeeDoctor = _employee.GetIdDoctorByUserId(horario.IdUserDoctor);
            if (IdEmployeeDoctor.Result == null)
            {
                throw new Exception("El Id User del empleado no existe o no es Doctor");
            }
            if (ExisteHorarioAsync(horario).Result == false)
            {
                Horario horario1 = new Horario
                {
                    HoraInicio = horario.HoraInicio,
                    IdUsuarioCreacion = horario.IdUserDoctor,
                    HoraFin = horario.HoraFin,
                    IdDoctor = IdEmployeeDoctor.Id,
                    Version = 1,
                    DiaSemana = horario.DiaSemana,
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

        private async Task<bool> ExisteHorarioAsync(HorarioDTO horario)
        {
            return await _context.Horarios.Include(c=> c.Doctor).AnyAsync(h =>
                h.Doctor.UserId == horario.IdUserDoctor &&
                h.DiaSemana == horario.DiaSemana &&
                ((horario.HoraInicio >= h.HoraInicio && horario.HoraInicio < h.HoraFin) ||
                 (horario.HoraFin > h.HoraInicio && horario.HoraFin <= h.HoraFin) ||
                 (horario.HoraInicio <= h.HoraInicio && horario.HoraFin >= h.HoraFin)));
        }
    }
}
