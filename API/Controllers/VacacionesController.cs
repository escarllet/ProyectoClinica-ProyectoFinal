using Application.DTOs.Request.Vacaciones;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllVacacionesAsync(string? NombreEmpleado = null, int? EmployeId = null, string? estado = null)
        {
            try
            {
                var users = await _services.GetAllVacacionesAsync(NombreEmpleado,EmployeId,estado);
                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        
        [HttpGet("GetEstados")]
        [AllowAnonymous]
        // [Authorize(Roles = "Admin")]
        public IActionResult GetEstadosSolicitudes()
        {
            var users = _services.GetAllEstadosVacaciones();
            return Ok(users);
        }
        //Como administrador, quiero poder aprobar una solicitud
        //de vacaciones para que el empleado pueda tomar su descanso.
        [HttpPut("aprobar")]
        [AllowAnonymous]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AprobarSolicitud(int Id)
        {
           
            var result = await _services.AprobarSolicitudAsync(Id);

            if (!result)
                return BadRequest("No se pudo aprobar la solicitud.");

            return Ok("Solicitud aprobada correctamente.");
        }
        //Como administrador, quiero poder rechazar una solicitud de vacaciones si no cumple con los
        //requisitos o si hay conflicto en la planificación.
        [HttpPut("denegar")]
        [AllowAnonymous]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DenegarSolicitud(int id)
        {

            var result = await _services.DenegarSolicitudAsync(id);

            if (!result)
                return BadRequest("No se pudo denegar la solicitud.");

            return Ok("Solicitud denegada correctamente.");
        }  
        [HttpPost("solicitar")]
        [AllowAnonymous]
        // [Authorize(Roles = "employee")]
        public async Task<IActionResult> SolicitarVacaciones(InsertVacaciones insertVacaciones)
        {
            try
            {
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
        [AllowAnonymous]
        // [Authorize(Roles = "employee")]
        public async Task<IActionResult> CancelarVacaciones(int VacacionesId)
        {
            try
            {
                var result = await _services.CancelarVacaciones(VacacionesId);

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
