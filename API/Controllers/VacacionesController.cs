using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllVacacionesAsync()
        {
            var users = _services.GetAllVacacionesAsync();
            return Ok(users);
        }
        //Como administrador, quiero ver el historial de vacaciones
        //de los empleados para llevar un control adecuado.
        [HttpGet("GetBy")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ObtenerHistorialVacaciones([FromQuery] string? NombreCompleto, [FromQuery] string? estado)
        {
            var historial = await _services.ObtenerHistorialVacacionesAsync(NombreCompleto, estado);

            if (!historial.Any())
                return NotFound("No se encontraron solicitudes de vacaciones con los filtros aplicados.");

            return Ok(historial);
        }
        [HttpGet("GetEstados")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetEstadosSolicitudes()
        {
            var users = _services.GetAllEstadosVacaciones();
            return Ok(users);
        }
        //Como administrador, quiero poder aprobar una solicitud
        //de vacaciones para que el empleado pueda tomar su descanso.
        [HttpPut("aprobar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AprobarSolicitud(int id)
        {
           
            var result = await _services.AprobarSolicitudAsync(id);

            if (!result)
                return BadRequest("No se pudo aprobar la solicitud.");

            return Ok("Solicitud aprobada correctamente.");
        }
        //Como administrador, quiero poder rechazar una solicitud de vacaciones si no cumple con los
        //requisitos o si hay conflicto en la planificación.
        [HttpPut("denegar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DenegarSolicitud(int id)
        {

            var result = await _services.DenegarSolicitudAsync(id);

            if (!result)
                return BadRequest("No se pudo denegar la solicitud.");

            return Ok("Solicitud denegada correctamente.");
        }
    }
}
