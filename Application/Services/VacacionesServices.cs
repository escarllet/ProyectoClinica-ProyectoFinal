using Application.Contracts;
using Application.DTOs.Response.Vacaciones;
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
        public VacacionesServices(IVacacionesRepository repository)
        {
            _repository = repository;
        }


        public Task<List<VacacionesDTO>> GetAllVacacionesAsync(string? NombreEmpleado = null, int? EmployeId = null, string? estado = null)
        {
            return  _repository.GetAllVacacionesAsync(NombreEmpleado,EmployeId,estado);
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

    }
}
