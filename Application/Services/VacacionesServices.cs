using Application.Contracts;
using Application.DTOs.Request.Vacaciones;
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


        public Task<List<VacacionesDTO>> GetAllVacacionesAsync(string? NombreEmpleado = null, string? estado = null)
        {
            return  _repository.GetAllVacacionesAsync(NombreEmpleado,estado);
        } 
        public Task<List<VacacionesDTO>> GetMisVacacionesAsync(string UserId,string? estado = null)
        {
            return  _repository.GetMisVacacionesAsync(UserId,estado);
        } 
        public string[] GetAllEstadosVacaciones()
        {
            string[] Estados = {
                "Pendiente", "Aprobado", "Denegado", "Cancelado" };              
            return Estados;
        }
        public Task<bool> AprobarSolicitudAsync(int solicitudId, string IdUser)
        {
            return _repository.AprobarSolicitudAsync(solicitudId,IdUser);
        } 
        public Task<bool> DenegarSolicitudAsync(int solicitudId, string IdUser)
        {
            return _repository.DenegarSolicitudAsync(solicitudId, IdUser);
        }   
        public Task<bool> SolicitarVacaciones(InsertVacaciones insertVacaciones)
        {
            return _repository.SolicitarVacaciones(insertVacaciones);
        }  
        public Task<bool> CancelarVacaciones(int VacacionesId,string IdUser)
        {
            return _repository.CancelarVacaciones(VacacionesId,IdUser);
        }   

    }
}
