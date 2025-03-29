using Domain.Entities.People;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Contracts;
using Application.DTOs.Request.Employee;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ClinicContext _context;
        private readonly IAuthService _authService;
        public EmployeeRepository(ClinicContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employees.Where(c => c.Activo).ToListAsync();
        }
        public async Task<List<Doctor>> GetAllDoctoresAsync()
        {
            return await _context.Doctores.Where(c => c.Activo).ToListAsync();
        }
        public async Task<List<DoctorSustituto>> GetAllDoctoresSustitutosAsync()
        {
            return await _context.DoctoresSustitutos.Where(c => c.Activo).ToListAsync();
        }

        public async Task<string> RegisterEmployeeAsync(RegisterEmployeeDto dto)
        {
            var employee = _authService.RegisterUserEmployeAsync(dto);

            _context.Employees.Add(employee.Result);
            await _context.SaveChangesAsync();

            return "Usuario y empleado creados correctamente.";
        }
    }
}
