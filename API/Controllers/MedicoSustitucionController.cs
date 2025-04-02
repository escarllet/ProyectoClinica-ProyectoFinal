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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AsignarSustituto(ObtenerSustituciones obtener)
        {
            var employees = await _service.AsignarSustitutoAsync(obtener);
            return Ok(employees);
        }
        //Como administrador, quiero poder ver todas las sustituciones activas
        //y pasadas para gestionar los reemplazos de manera eficiente.
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Sustituciones>>> GetAllReplacements()
        {
            var replacements = await _service.GetAllReplacementsAsync();
            return Ok(replacements);
        }
        //Como administrador, quiero poder ver todas las sustituciones activas
        [HttpGet("GetActive")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Sustituciones>>> GetActiveReplacementsAsync()
        {
            var replacements = await _service.GetActiveReplacementsAsync();
            return Ok(replacements);
        }
        //Como administrador, quiero poder editar la información de una sustitución en caso de cambios.
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSustitucion(int id, [FromBody] UpdateSustitucionDto dto)
        {
            if (id != dto.Id) return BadRequest("El ID en la URL y en el cuerpo deben coincidir.");

            var resultado = await _service.UpdateSustitucionAsync(dto);
            if (!resultado) return NotFound("No se encontró la sustitución.");

            return Ok(new { message = "Sustitucion actualizada correctamente" });
        }
        //Como administrador, quiero poder eliminar una sustitución en caso de que no sea necesaria.
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSustitucion(int id)
        {
            var resultado = await _service.DeleteSustitucionAsync(id);
            if (!resultado) return NotFound("No se encontró la sustitución.");

            return Ok(new { message = "Sustitucion Eliminada correctamente" });
        }
    }
}
