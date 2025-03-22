using Domain.Entities.People;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeeController(EmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetEmployees()
    {
        var employees = await _service.GetEmployeesAsync();
        return Ok(employees);
    }
}
