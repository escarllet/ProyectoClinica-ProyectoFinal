using Domain.Entities.People;
using Application.Contracts;
using Application.DTOs.Request.Employee;
using Application.DTOs.Request.User;
using System.Threading.Tasks;
using Application.DTOs.Response.Employee;
namespace Application.Services
{ 
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public List<UsuarioPerfilDto> GetAllEmployeeAsync(string? filtro = null)
        {
            return  _repository.GetAllEmployeeAsync(filtro);

        }
        public async Task<UsuarioPerfilDto> GetMyPerfilasync(string UserId)
        {
            return await _repository.GetMyPerfilasync(UserId);

        }
        public string[] GetRoles()
        {

           return ValidateToken.GetRoles();

        }
        public async Task<string> RegisterUserEmployeAsync(RegisterEmployeeDto emplo)
        {
            return await _repository.RegisterEmployeeAsync(emplo);
        }
        public async Task<bool> UpdateEmpleadoAsync(UpdateEmployeeDto dto)
        {
            return await _repository.UpdateEmpleadoAsync(dto);
        }
        public async Task<bool> DeleteEmpleadoAsync(DeleteEmployeeDTO deleteEmployee)
        {
            return await _repository.DeleteEmpleadoAsync(deleteEmployee);
        }
        public async Task<bool> ActivarEmpleadoAsync(int EmployeeId)
        {
            return await _repository.ActivarEmpleadoAsync(EmployeeId);
        }
        public async Task<List<DoctorDto>> GetAllNoSustituteDoctor()
        {
            return await _repository.GetAllNoSustituteDoctor();
        }
        public async Task<List<DoctorDto>> GetAllDoctoresSustitutosAsync()
        {
            return await _repository.GetAllDoctoresSustitutosAsync();
        }
    }
}
