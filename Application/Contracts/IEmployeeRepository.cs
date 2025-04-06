using Application.DTOs.Request.Employee;
using Application.DTOs.Request.User;
using Domain.Entities.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IEmployeeRepository
    {
        List<UsuarioPerfilDto> GetAllEmployeeAsync(string? filtro = null);
        Task<string> RegisterEmployeeAsync(RegisterEmployeeDto dto);
        Task<List<Doctor>> GetAllDoctoresAsync();
        Task<UsuarioPerfilDto> GetMyPerfilasync(string UserId);
        Task<bool> UpdateEmpleadoAsync(UpdateEmployeeDto dto);
        Task<bool> DeleteEmpleadoAsync(DeleteEmployeeDTO deleteEmployee);
        Task<bool> ActivarEmpleadoAsync(int EmployeeId);
        Task<List<Doctor>> GetAllNoSustituteDoctor();
        Task<List<DoctorSustituto>> GetAllDoctoresSustitutosAsync();

    }
}
