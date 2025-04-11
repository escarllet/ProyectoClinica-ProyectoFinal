using Application.DTOs.Request.Employee;
using Application.DTOs.Response.Employee;
using Application.DTOs.Response.Sustituciones;
using Application.Services;
using Domain.Entities;
using Domain.Entities.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoSustitucionController : Controller
    {
        private readonly MedicoSustitucionService _service;
        public MedicoSustitucionController(MedicoSustitucionService service)
        {
            _service = service;
        }
        //Como administrador, quiero asignar un médico sustituto a un médico titular o interino,
        //definiendo la fecha de inicio y fin de la sustitución.
        [HttpPost("AsignarSustitucion")]
        public async Task<IActionResult> AsignarSustituto(ObtenerSustituciones obtener)
        {
            try
            {
                string[] rols = {"Admin"};
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                obtener.ModifyUserId = a.UserId;
                var employees = await _service.AsignarSustitutoAsync(obtener);
                return Ok("Doctor asignado correctamente!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        //Como administrador, quiero poder ver todas las sustituciones activas
        //y pasadas para gestionar los reemplazos de manera eficiente.
        //Como administrador, quiero poder ver todas las sustituciones activas
        [HttpGet("GetAllSustituciones")]
        public  ActionResult<List<GetSustituciones>> GetAllReplacements(bool OnlyActive)
        {
            string[] rols = { "Admin" };
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            var replacements = _service.GetAllReplacementsAsync(OnlyActive).Result;
            return Ok(replacements);
        }
        
        //Como administrador, quiero poder editar la información de una sustitución en caso de cambios.
        [HttpPut("UpdateSustitucion")]
        public async Task<IActionResult> UpdateSustitucion([FromBody] UpdateSustitucionDto dto)
        {
            try
            { 
                string[] rols = { "Admin" };
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                dto.ModifyUserId = a.UserId;
                var resultado = await _service.UpdateSustitucionAsync(dto);

                if (!resultado) return NotFound("No se encontró la sustitución.");

                return Ok(new { message = "Sustitucion actualizada correctamente" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        //Como administrador, quiero poder eliminar una sustitución en caso de que no sea necesaria.
        [HttpDelete("DeleteSustitucion")]
        public async Task<IActionResult> DeleteSustitucion(int Id)
        {
            string[] rols = { "Admin" };
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            var resultado = await _service.DeleteSustitucionAsync(Id,a.UserId);
            if (!resultado) return NotFound("No se encontró la sustitución.");

            return Ok(new { message = "Sustitucion Eliminada correctamente" });
        }
    }
}
