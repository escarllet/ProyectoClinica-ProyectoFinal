using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Request.Horario;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : Controller
    {
        private readonly HorarioService _service;
        public HorarioController(HorarioService service)
        {
            _service = service;
        }
        //Como médico interino/titular, quiero ver mi horario de consulta para organizar mi agenda.
        //Como médico sustituto, quiero ver el horario del médico al que estoy sustituyendo para conocer sus turnos.
        [HttpGet("GetHorario")]
        public async Task<IActionResult> ObtenerHorariosPorUsuarioAsync()
        {
            string[] rols = ["DoctorTitular","DoctorInterino","DoctorSustituto"];
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            var employees = await _service.ObtenerHorariosPorUsuarioAsync(a.UserId);
            return Ok(employees);

        }
        [HttpPost("AgregarHorario")]
        //ojo un usuario doctor sustituto no deberia poder agregar horarios
        public async Task<IActionResult> AgregarHorarioAsync( HorarioDTO horario)
        {
            try
            {
                string[] rols = ["DoctorTitular", "DoctorInterino"];
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                horario.IdUserDoctor = a.UserId;
                await _service.AgregarHorarioAsync(horario);
                return Ok("Horario agregado Correctamente");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        } 
        [HttpPut("EditarHorario")]
        //ojo un usuario doctor sustituto no deberia poder agregar horarios
        public async Task<IActionResult> EditarHorarioAsync( UpdateHorarioDTO Horario)
        {
            try
            {
                string[] rols = ["DoctorTitular", "DoctorInterino"];
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                Horario.IdUserDoctor = a.UserId;
                await _service.EditarHorarioAsync(Horario);
                return Ok("Horario Actualizado Correctamente");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
           
        }
        [HttpDelete("EliminarHorario")]
        //ojo un usuario doctor sustituto no deberia poder agregar horarios
        public async Task<IActionResult> EliminarHorarioAsync(int IdHorario)
        {
            try
            {
                string[] rols = ["DoctorTitular", "DoctorInterino"];
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                var idUserDoctor = a.UserId;
                await _service.EliminarHorarioAsync(IdHorario,idUserDoctor);
                return Ok("Horario Eliminado Correctamente");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
           
        }
       
    }
}
