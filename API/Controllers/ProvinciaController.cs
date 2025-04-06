using Application.DTOs.Response.Provincia;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ProvinciaService _service;
        public ProvinciaController(ProvinciaService service)
        {
            _service = service;
        }
        //Para poder agregar el empleado, necesito seleccionar alguna provincia de la lista.
        // tambien tiene filtro
        // ya funciona
        [HttpGet("AllProvincias")]
        public List<ProvinciaDTO> GetAllProvincias(string? provincia = null)
        {
            var provincias = _service.GetAllProvincias(provincia).Result;
            return provincias;
        }
    }
}
