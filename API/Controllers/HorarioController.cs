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
        public async Task<IActionResult> ObtenerHorariosPorUsuarioAsync(int DoctorId)
        {
            var employees = await _service.ObtenerHorariosPorUsuarioAsync(DoctorId);
            return Ok(employees);

        }
        [HttpPost]
        [AllowAnonymous]
        // [Authorize(Roles = "DoctorTitular")]
        //ojo un usuario doctor sustituto no deberia poder agregar horarios
        public async Task<IActionResult> AgregarHorarioAsync( HorarioDTO horario)
        {
            try
            {
                await _service.AgregarHorarioAsync(horario);
                return Ok("Horario agregado Correctamente");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
           
        } 
        [HttpPut]
        [AllowAnonymous]
        // [Authorize(Roles = "DoctorTitular")]
        //ojo un usuario doctor sustituto no deberia poder agregar horarios
        public async Task<IActionResult> EditarHorarioAsync( UpdateHorarioDTO Horario)
        {
            try
            {
                await _service.EditarHorarioAsync(Horario);
                return Ok("Horario Actualizado Correctamente");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
           
        }
        [HttpDelete]
        [AllowAnonymous]
        // [Authorize(Roles = "DoctorTitular")]
        //ojo un usuario doctor sustituto no deberia poder agregar horarios
        public async Task<IActionResult> EliminarHorarioAsync(int IdHorario)
        {
            try
            {
                await _service.EliminarHorarioAsync(IdHorario);
                return Ok("Horario Eliminado Correctamente");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
           
        }
       
    }
}
