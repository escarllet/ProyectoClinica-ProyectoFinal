using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IHorario
    {
        Task<List<Horario>> ObtenerHorariosPorUsuarioAsync(string idUsuario);
        Task AgregarHorarioAsync(Horario horario);
        Task<bool> ExisteHorarioAsync(string idUsuario, string diaSemana, TimeOnly horaInicio, TimeOnly horaFin);
    }
}
