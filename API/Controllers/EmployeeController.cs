using Domain.Entities.People;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Application.Contracts;
using Application.DTOs.Request.Employee;
using Domain.Entities;

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
 
    //Como administrador, quiero poder crear un nuevo usuario para registrar a nuevos empleados.
    // Le crea el rol y el usuario automaticamente
    [HttpPost("register-employee")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeDto dto)
    {
        var result = await _service.RegisterUserEmployeAsync(dto);

        if (result.Contains("Error"))
            return BadRequest(result);

        return Ok(result);
    }
    //Busca todos los empleados de tipo doctores
    [HttpPost("Doctores/GetAll")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllDoctoresAsync()
    {
        var result = await _service.GetAllDoctoresAsync();
        return Ok(result);
    }
    
}
