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
        //Como médico interino/titular, quiero registrar un paciente para asignarlo a mi lista de atención.
        //Como médico sustituto, quiero registrar pacientes mientras realizo una sustitución.
        [HttpPost]
        [AllowAnonymous]
        // [Authorize(Roles = "Doctor")] 
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
        //Como médico sustituto, quiero ver la lista de pacientes del médico al que sustituyo para atenderlos correctamente.
        [HttpPut]
        [AllowAnonymous]
        // [Authorize(Roles = "Doctor")] 
        public async Task<IActionResult> EditarPaciente(PacienteDTO paciente)
        {
            try
            {
                var paci = await _service.EditarPaciente(paciente);
                return Ok("Paciente Actualizado Correctamente");
            }
            catch (Exception es)
            {

                return BadRequest(es.Message);
            }

        }
        [HttpDelete]
        [AllowAnonymous]
        // [Authorize(Roles = "DoctorTitular,DoctorInterino")] 
        public async Task<IActionResult> EliminarPaciente(int paciente)
        {
            try
            {
                var paci = await _service.EliminarPaciente(paciente);
                return Ok("Paciente Eliminado Correctamente");
            }
            catch (Exception es)
            {

                return BadRequest(es.Message);
            }

        }
            [HttpGet]
            [AllowAnonymous]
            // [Authorize(Roles = "DoctorTitular,DoctorInterino")] 
            public async Task<IActionResult> VerMisPaciente(int DoctorId, string? filtro = null)
            {
                try
                {
                var paci = await _service.VerMisPacientes(DoctorId,filtro);
                    return Ok(paci);
                }
                catch (Exception es)
                {

                    return BadRequest(es.Message);
                }

            }

        }
}
