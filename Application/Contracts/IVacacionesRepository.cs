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
       
        Task<bool> AprobarSolicitudAsync(int solicitudId);
        Task<bool> DenegarSolicitudAsync(int solicitudId);
        Task<List<VacacionesDTO>> GetAllVacacionesAsync(string? NombreEmpleado = null, int? EmployeId = null, string? estado = null);
        Task<bool> SolicitarVacaciones(InsertVacaciones insertVacaciones);
        Task<bool> CancelarVacaciones(int VacacionesId);
    }
}
