using Domain.Entities.People;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeeController(EmployeeService service)
    {
        _service = service;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetEmployees()
    {
        var employees = await _service.GetEmployeesAsync();
        return Ok(employees);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("SoloAdmins")]
    public IActionResult SoloParaAdmins()
    {
        return Ok("Este endpoint solo puede ser accedido por Administradores.");
    }
    [Authorize(Roles = "Admin,Doctor")]
    [HttpGet("SoloAdminsYDoctores")]
    public IActionResult SoloParaAdminsYDoctores()
    {
        return Ok("Este endpoint puede ser accedido por Administradores y Doctores.");
    }
    [AllowAnonymous] // Cualquiera puede acceder sin autenticación
    [HttpGet("PublicInfo")]
    public IActionResult PublicInfo()
    {
        return Ok("Información pública");
    }
}
