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
    }
}
