using Application.DTOs.Request.Vacaciones;
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
       
        Task<bool> AprobarSolicitudAsync(int solicitudId,string IdUser);
        Task<bool> DenegarSolicitudAsync(int solicitudId,string IdUser);
        Task<bool> CancelarVacaciones(int VacacionesId, string IdUser);
        Task<List<VacacionesDTO>> GetAllVacacionesAsync(string? NombreEmpleado = null, string? estado = null);
        Task<bool> SolicitarVacaciones(InsertVacaciones insertVacaciones);
        Task<List<VacacionesDTO>> GetMisVacacionesAsync(string UserId, string? estado = null);
    }
}
