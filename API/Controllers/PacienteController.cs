using Application.DTOs.Request.Paciente;
using Application.DTOs.Response.Provincia;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly PacienteService _service;
        public PacienteController(PacienteService service)
        {
            _service = service;
        }
        [HttpGet]
        [AllowAnonymous]
        // [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> InsertarPaciente(InsertPacienteDTO paciente)
        {
            try
            {
                var paci = await _service.InsertarPaciente(paciente);
                return Ok("Paciente Insertado Correctamente");
            }
            catch (Exception es)
            {

                return BadRequest(es.Message);
            }
            
        }

    }
}
