using Domain.Entities.People;
using Application.Contracts;
using Application.DTOs.Request.Employee;
namespace Application.Services
{ 
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _repository.GetAllEmployeeAsync();

        }
       public async Task<string> RegisterUserEmployeAsync(RegisterEmployeeDto emplo)
        {
            return await _repository.RegisterEmployeeAsync(emplo);
        }
        public async Task<List<Doctor>> GetAllDoctoresAsync()
        {
            return await _repository.GetAllDoctoresAsync();
        }
    }
}
