using Application.DTOs.Request.Vacaciones;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacacionesController : Controller
    {
        private readonly VacacionesServices _services;
        public VacacionesController(VacacionesServices service)
        {
            _services = service;
        }
        //Como administrador, quiero ver todas las solicitudes
        //de vacaciones de los empleados para gestionar su aprobación.
        //Como administrador, quiero ver el historial de vacaciones
        //de los empleados para llevar un control adecuado.
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllVacacionesAsync(string? NombreEmpleado = null, string? estado = null)
        {
            try
            {
                string[] rols = {"Admin"};
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                var users = await _services.GetAllVacacionesAsync(NombreEmpleado,estado);
                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("GetMisVacaciones")]
        public async Task<IActionResult> GetMisVacacionesAsync(string? estado = null)
        {
            try
            {
                string[] rols = {"Admin", "DoctorInterino", "DoctorTitular",
                "AuxEnfermeria","ATS", "ATSZona","Celadores"};
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }

                var users = await _services.GetMisVacacionesAsync(a.UserId,estado);
                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetEstados")]
        public IActionResult GetEstadosSolicitudes()
        {
            var users = _services.GetAllEstadosVacaciones();
            return Ok(users);
        }
        //Como administrador, quiero poder aprobar una solicitud
        //de vacaciones para que el empleado pueda tomar su descanso.
        [HttpPut("aprobar")]
        public async Task<IActionResult> AprobarSolicitud(int Id)
        {
            try
            {
                string[] rols = {"Admin"};
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                var result = await _services.AprobarSolicitudAsync(Id,a.UserId);

                if (!result)
                    return BadRequest("No se pudo aprobar la solicitud.");

                return Ok("Solicitud aprobada correctamente.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        //Como administrador, quiero poder rechazar una solicitud de vacaciones si no cumple con los
        //requisitos o si hay conflicto en la planificación.
        [HttpPut("denegar")]
        public async Task<IActionResult> DenegarSolicitud(int id)
        {
            try
            {
                string[] rols = { "Admin" };
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                var result = await _services.DenegarSolicitudAsync(id,a.UserId);

                if (!result)
                    return BadRequest("No se pudo denegar la solicitud.");

                return Ok("Solicitud denegada correctamente.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }  
        [HttpPost("solicitar")]
        public async Task<IActionResult> SolicitarVacaciones(InsertVacaciones insertVacaciones)
        {
            try
            {
                string[] rols = {
                "Admin", "DoctorInterino", "DoctorTitular",
                "AuxEnfermeria","ATS", "ATSZona","Celadores"};
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                insertVacaciones.EmployeeUserId = a.UserId;
                var result = await _services.SolicitarVacaciones(insertVacaciones);

                if (!result)
                    return BadRequest("No se pudo insertar la solicitud.");

                return Ok("Solicitud insertada correctamente.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut("cancelar")]
        public async Task<IActionResult> CancelarVacaciones(int VacacionesId)
        {
            try
            {
                string[] rols = {
                "Admin", "DoctorInterino", "DoctorTitular",
                "AuxEnfermeria","ATS", "ATSZona","Celadores"};
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }

                var result = await _services.CancelarVacaciones(VacacionesId,a.UserId);

                if (!result)
                    return BadRequest("No se pudo cancelar la solicitud.");

                return Ok("Solicitud cancelada correctamente.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}
