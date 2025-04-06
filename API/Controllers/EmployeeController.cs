using Domain.Entities.People;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs.Request.Employee;
using Application.Contracts;
using Domain.Entities;
using Application.DTOs.Request.User;
using System.Threading.Tasks;


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
    [HttpGet("GetEmployee")]
    public  List<UsuarioPerfilDto> GetEmployees(string? filtro = null)
    {

        string[] rols = ["Admin"];
        var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
        if (!a.IsValidUser)
        {
            throw new Exception("Usuario no tiene permisos para realiza esta accion");
        }
        var employees =  _service.GetAllEmployeeAsync(filtro);
        return employees;
    }

    [HttpGet("GetMiPerfil")]
    public async Task<UsuarioPerfilDto> GetMyPerfil()
    {
        string[] rols = {
                "Admin", "DoctorSustituto", "DoctorInterino", "DoctorTitular",
                "AuxEnfermeria","ATS", "ATSZona","Celadores"};

        var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
        if (!a.IsValidUser)
        {
            throw new Exception("Usuario no tiene permisos para realiza esta accion");
        }
        var employees = await _service.GetMyPerfilasync(a.UserId);
        return employees;
    }

    //se busca la lista de roles
    //ya funciona
    [HttpGet("GetTipoEmployee")]
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
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeDto dto)
    {
        string[] rols = ["Admin"];
        var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
        if (!a.IsValidUser)
        {
            return Unauthorized("Usuario no tiene permisos para realiza esta accion");
        }
        dto.UsuarioCreacion = a.UserId;
        var result = await _service.RegisterUserEmployeAsync(dto);

        if (result.Contains("Error"))
            return BadRequest(result);

        return Ok(result);
    }


    //Como administrador, quiero poder editar la información de un empleado
    //para mantener la información al día.
    //ya funciona
    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateEmpleado([FromBody] UpdateEmployeeDto dto)
    {
        try
        {
            string[] rols = ["Admin"];
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            dto.UsuarioModificacion = a.UserId;
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
    [HttpDelete("DeleteEmployee")]
    public async Task<IActionResult> DeleteEmpleado([FromBody] DeleteEmployeeDTO deleteEmployee)
    {
        try
        {
            string[] rols = ["Admin"];
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            deleteEmployee.IdUserEmployee = a.UserId;
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
    public async Task<IActionResult> ActivarEmpleado(int IdEmployee)
    {
        try
        {
            string[] rols = ["Admin"];
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            var resultado = await _service.ActivarEmpleadoAsync(IdEmployee);
            if (!resultado) return NotFound("No se encontró el empleado.");

            return Ok(new { message = "Empleado y usuario Activado correctamente" });
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    //Busca todos los empleados de tipo doctores sustitutos
    [HttpGet("DoctoresSustitutos/GetAll")]
    public async Task<IActionResult> GetAllDoctoresSutitutos()
    {
        try
        {
            string[] rols = ["Admin"];
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            var resultado = await _service.GetAllDoctoresSustitutosAsync();
            if (resultado.Count() == 0) return NotFound("No se encontró el empleado.");

            return Ok(resultado);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    //Busca todos los empleados de tipo doctores no sustitutos
    [HttpGet("DoctoresNoSustitutos/GetAll")]
    public async Task<IActionResult> GetAllNoSustituteDoctor()
    {
        try
        {
            string[] rols = ["Admin"];
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            var resultado = await _service.GetAllNoSustituteDoctor();
            if (resultado.Count() == 0) return NotFound("No se encontró el empleado.");

            return Ok(resultado);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
 

}
