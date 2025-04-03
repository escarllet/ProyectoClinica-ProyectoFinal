using Application.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HorarioService
    {
        private readonly IHorario _horario;
        public HorarioService(IHorario horario)
        {
            _horario = horario;
        }
        public async Task<List<Horario>> ObtenerHorariosPorUsuarioAsync(string id)
        {
            return await _horario.ObtenerHorariosPorUsuarioAsync(id);
        }
    }
}
