using Application.DTOs.Request.Horario;
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
        Task AgregarHorarioAsync(HorarioDTO horario);
        Task<List<Horario>> ObtenerHorariosPorUsuarioAsync(string idUsuario);
    }
}
