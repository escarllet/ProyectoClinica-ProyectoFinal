using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ProvinciaService _provinciaService;

        //Para poder agregar el empleado, necesito seleccionar alguna provincia de la lista.
        [HttpGet("AllProvincias")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllProvincias()
        {
            var users =  _provinciaService.GetAllProvincias();
            return Ok(users);
        }
    }
}
