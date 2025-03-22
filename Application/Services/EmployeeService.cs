using Domain.Entities.People;
using Application.Contracts;
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
    }
}
