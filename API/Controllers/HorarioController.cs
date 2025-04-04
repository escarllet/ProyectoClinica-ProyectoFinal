using Application.DTOs.Response.Employee;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.DTOs.Request.Horario;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : Controller
    {
        private readonly HorarioService _service;
        public HorarioController(HorarioService service)
        {
            _service = service;
        }
        [HttpGet]
        [AllowAnonymous]
        // [Authorize(Roles = "DoctorTitular")]
        //falta validar sustituciones
        public async Task<IActionResult> ObtenerHorariosPorUsuarioAsync(string UserId)
        {
            var employees = await _service.ObtenerHorariosPorUsuarioAsync(UserId);
            return Ok(employees);

        }
        [HttpPost]
        [AllowAnonymous]
        // [Authorize(Roles = "DoctorTitular")]
        //ojo un usuario doctor sustituto no deberia poder agregar horarios
        public async Task<IActionResult> AgregarHorarioAsync( HorarioDTO UserId)
        {
            try
            {
                await _service.AgregarHorarioAsync(UserId);
                return Ok("Horario agregado Correctamente");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
           
        }
       
    }
}
