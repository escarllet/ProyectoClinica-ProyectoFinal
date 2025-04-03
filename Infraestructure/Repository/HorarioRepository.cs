using Application.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class HorarioRepository : IHorario
    {
        private readonly ClinicContext _context;
        public HorarioRepository(ClinicContext context)
        {
            _context = context;

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
        public async Task AgregarHorarioAsync(Horario horario)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteHorarioAsync(string idUsuario, string diaSemana, TimeOnly horaInicio, TimeOnly horaFin)
        {
            return await _context.Horarios.AnyAsync(h =>
                h.Doctor != null && h.Doctor.IdUsuarioCreacion == idUsuario &&
                h.DiaSemana == diaSemana &&
                ((horaInicio >= h.HoraInicio && horaInicio < h.HoraFin) ||
                 (horaFin > h.HoraInicio && horaFin <= h.HoraFin) ||
                 (horaInicio <= h.HoraInicio && horaFin >= h.HoraFin)));
        }
    }
}
