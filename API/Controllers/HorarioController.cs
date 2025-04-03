using Application.DTOs.Response.Employee;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet]
       // [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> ObtenerHorariosPorUsuarioAsync(string UserId)
        {
            var employees = await _service.ObtenerHorariosPorUsuarioAsync(UserId);
            return Ok(employees);

        }
       
    }
}
