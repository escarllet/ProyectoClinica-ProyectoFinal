using Domain.Entities.People;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs.Request.Employee;
using Application.Contracts;
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

    //Como administrador, quiero ver la lista de empleados registrados para gestionar su información.
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Employee>>> GetEmployees()
    {
        var employees = await _service.GetEmployeesAsync();
        return Ok(employees);
    }

    //se busca la lista de roles
    [HttpGet("GetTipoEmployee")]
    [AllowAnonymous]
    public IActionResult GetRoles()
    {
        var roles = _service.GetRoles();
        return Ok(roles);
    }

    //Como administrador, quiero poder crear un nuevo usuario para registrar a nuevos empleados.
    // Le crea el rol y el usuario automaticamente
    //Como administrador, quiero poder crear un nuevo registro de empleado para actualizar la base de datos.
    //la realidad es que se crea el empleado, y el usuario con el rol correspondiente automaticamente.
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
    [HttpGet("Doctores/GetAll")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllDoctoresAsync()
    {
        var result = await _service.GetAllDoctoresAsync();
        return Ok(result);
    }
    //Como administrador, quiero poder editar la información de un empleado
    //para mantener la información al día.
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateEmpleado([FromBody] UpdateEmployeeDto dto)
    {
        var resultado = await _service.UpdateEmpleadoAsync(dto);
        if (!resultado) return NotFound("No se encontró el empleado.");

        return Ok(new { message = "Empleado Actualizado correctamente" });
    }
    //Como administrador, quiero poder eliminar un empleado
    //si ya no trabaja en el centro de salud.
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEmpleado([FromBody] int id, DateTime fechaSalida)
    {
        var resultado = await _service.DeleteEmpleadoAsync(id, fechaSalida);
        if (!resultado) return NotFound("No se encontró el empleado.");

        return Ok(new { message = "Empleado Inactivado correctamente" });
    }

}
