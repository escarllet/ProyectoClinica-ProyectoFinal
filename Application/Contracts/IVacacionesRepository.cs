using Application.DTOs.Response.Vacaciones;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IVacacionesRepository
    {
        Task<List<VacacionesDTO>> GetAllVacacionesAsync();
        Task<bool> AprobarSolicitudAsync(int solicitudId);
        Task<bool> DenegarSolicitudAsync(int solicitudId);
        Task<List<Vacaciones>> ObtenerHistorialVacacionesAsync(string? correoEmpleado, string? estado);
    }
}
