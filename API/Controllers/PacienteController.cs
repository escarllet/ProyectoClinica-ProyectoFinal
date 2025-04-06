using Application.DTOs.Request.Paciente;
using Application.DTOs.Response.Provincia;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        [HttpPost("InsertPaciente")]
        public async Task<IActionResult> InsertarPaciente(InsertPacienteDTO paciente)
        {
            try
            {
                string[] rols = ["DoctorTitular", "DoctorInterino","DoctorSustituto"];
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                paciente.IdUserDoctor = a.UserId;
                var paci = await _service.InsertarPaciente(paciente);
                return Ok("Paciente Insertado Correctamente");
            }
            catch (Exception es)
            {

                return BadRequest(es.Message);
            }
            
        }
        //Como médico sustituto, quiero ver la lista de pacientes del médico al que sustituyo para atenderlos correctamente.
        [HttpPut("UpdatePaciente")]
        public async Task<IActionResult> EditarPaciente(PacienteDTO paciente)
        {
            try
            {
                string[] rols = ["DoctorTitular", "DoctorInterino"];
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                paciente.UserDoctorId = a.UserId;
                var paci = await _service.EditarPaciente(paciente);
                return Ok("Paciente Actualizado Correctamente");
            }
            catch (Exception es)
            {

                return BadRequest(es.Message);
            }

        }
        [HttpDelete("DeletePaciente")]
        public async Task<IActionResult> EliminarPaciente(int paciente)
        {
            try
            {
                string[] rols = ["DoctorTitular", "DoctorInterino"];
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                var IdUserDoctor = a.UserId;
                var paci = await _service.EliminarPaciente(paciente, IdUserDoctor);
                return Ok("Paciente Eliminado Correctamente");
            }
            catch (Exception es)
            {

                return BadRequest(es.Message);
            }

        }
            [HttpGet("GetMisPacientes")]
            public async Task<IActionResult> VerMisPaciente(string? filtro = null)
            {
                try
                {
                string[] rols = ["DoctorTitular", "DoctorInterino", "DoctorSustituto"];
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                var paci = await _service.VerMisPacientes(a.UserId,filtro);
                    return Ok(paci);
                }
                catch (Exception es)
                {

                    return BadRequest(es.Message);
                }

            }

        }
}
