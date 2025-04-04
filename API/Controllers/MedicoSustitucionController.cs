using Application.DTOs.Request.Employee;
using Application.DTOs.Response.Employee;
using Application.Services;
using Domain.Entities;
using Domain.Entities.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
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
        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AsignarSustituto(ObtenerSustituciones obtener)
        {
            try
            {
                var employees = await _service.AsignarSustitutoAsync(obtener);
                return Ok(employees);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        //Como administrador, quiero poder ver todas las sustituciones activas
        //y pasadas para gestionar los reemplazos de manera eficiente.
        //Como administrador, quiero poder ver todas las sustituciones activas
        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ObtenerSustituciones>>> GetAllReplacements(bool OnlyActive,int? IdDoctor = null)
        {
            var replacements = await _service.GetAllReplacementsAsync(OnlyActive, IdDoctor);
            return Ok(replacements);
        }
        
        //Como administrador, quiero poder editar la información de una sustitución en caso de cambios.
        [HttpPut]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSustitucion([FromBody] UpdateSustitucionDto dto)
        {
            try
            {
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
        [HttpDelete]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSustitucion(int Id)
        {
            var resultado = await _service.DeleteSustitucionAsync(Id);
            if (!resultado) return NotFound("No se encontró la sustitución.");

            return Ok(new { message = "Sustitucion Eliminada correctamente" });
        }
    }
}
