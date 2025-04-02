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
        public string[] GetRoles()
        {

            string[] roles = {
                "Admin", "DoctorSustituto", "DoctorInterino", "DoctorTitular",
                "AuxEnfermeria","ATS", "ATSZona","Celadores"};
            return roles;

        }
        public async Task<string> RegisterUserEmployeAsync(RegisterEmployeeDto emplo)
        {
            return await _repository.RegisterEmployeeAsync(emplo);
        }
        public async Task<List<Doctor>> GetAllDoctoresAsync()
        {
            return await _repository.GetAllDoctoresAsync();
        }
        public async Task<bool> UpdateEmpleadoAsync(UpdateEmployeeDto dto)
        {
            return await _repository.UpdateEmpleadoAsync(dto);
        }
        public async Task<bool> DeleteEmpleadoAsync(int id, DateTime fechaSalida)
        {
            return await _repository.DeleteEmpleadoAsync(id,fechaSalida);
        }
    }
}
