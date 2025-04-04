using Domain.Entities.People;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs.Request.Employee;
using Application.Contracts;
using Domain.Entities;
using Application.DTOs.Request.User;

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
    //ya funciona
    [HttpGet]
    [AllowAnonymous]
    // [Authorize(Roles = "Admin")]
    public  List<UsuarioPerfilDto> GetEmployees(string? filtro = null)
    {
        var employees =  _service.GetAllEmployeeAsync(filtro);
        return employees;
    }

    //se busca la lista de roles
    //ya funciona
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
    //ya funciona
    [HttpPost("register-employee")]
    [AllowAnonymous]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeDto dto)
    {
        var result = await _service.RegisterUserEmployeAsync(dto);

        if (result.Contains("Error"))
            return BadRequest(result);

        return Ok(result);
    }
    //Busca todos los empleados de tipo doctores
    //ya funciona
    [HttpGet("Doctores/GetAll")]
    [AllowAnonymous]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllDoctoresAsync()
    {
        var result = await _service.GetAllDoctoresAsync();
        return Ok(result);
    }
    //Como administrador, quiero poder editar la información de un empleado
    //para mantener la información al día.
    //ya funciona
    [HttpPut]
    [AllowAnonymous]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateEmpleado([FromBody] UpdateEmployeeDto dto)
    {
        try
        {
            var resultado = await _service.UpdateEmpleadoAsync(dto);
            if (!resultado) return NotFound("No se encontró el empleado.");

            return Ok(new { message = "Empleado Actualizado correctamente" });
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
        
    }
    //Como administrador, quiero poder eliminar un empleado
    //si ya no trabaja en el centro de salud.
    // tambien elimina el user
    //ya funciona
    [HttpDelete]
    [AllowAnonymous]
    // [Authorize(Roles = "Admin")
    public async Task<IActionResult> DeleteEmpleado([FromBody] DeleteEmployeeDTO deleteEmployee)
    {
        try
        {
            var resultado = await _service.DeleteEmpleadoAsync(deleteEmployee);
            if (!resultado) return NotFound("No se encontró el empleado.");

            return Ok(new { message = "Empleado y usuario Inactivado correctamente" });
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
      
    }
    [HttpPut("ActivarEmployee")]
    [AllowAnonymous]
    // [Authorize(Roles = "Admin")
    public async Task<IActionResult> ActivarEmpleado(int IdEmployee)
    {
        try
        {
            var resultado = await _service.ActivarEmpleadoAsync(IdEmployee);
            if (!resultado) return NotFound("No se encontró el empleado.");

            return Ok(new { message = "Empleado y usuario Activado correctamente" });
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }

}
