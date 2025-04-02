using Application.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VacacionesServices
    {
        private readonly IVacacionesRepository _repository;

        public  Task<List<Vacaciones>> GetAllVacacionesAsync()
        {
            return  _repository.GetAllVacacionesAsync();
        } 
        public string[] GetAllEstadosVacaciones()
        {
            string[] Estados = {
                "Pendiente", "Aprobado", "Denegado", "Cancelado" };              
            return Estados;
        }
        public Task<bool> AprobarSolicitudAsync(int solicitudId)
        {
            return _repository.AprobarSolicitudAsync(solicitudId);
        } 
        public Task<bool> DenegarSolicitudAsync(int solicitudId)
        {
            return _repository.DenegarSolicitudAsync(solicitudId);
        }   
        public Task<List<Vacaciones>> ObtenerHistorialVacacionesAsync(string? NombreEmpleado, string? estado)
        {
            return _repository.ObtenerHistorialVacacionesAsync(NombreEmpleado, estado);
        }
    }
}
