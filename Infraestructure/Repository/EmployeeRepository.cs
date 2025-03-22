using Domain.Entities.People;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Contracts;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ClinicContext _context;

        public EmployeeRepository(ClinicContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employees.ToListAsync();
        }
    }
}
