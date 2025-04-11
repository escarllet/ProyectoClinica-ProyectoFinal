using Application.DTOs.Request.Employee;
using Application.DTOs.Request.User;
using Application.DTOs.Response.Employee;
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
        Task<List<DoctorDto>> GetAllNoSustituteDoctor();
        Task<string> RegisterEmployeeAsync(RegisterEmployeeDto dto);
        Task<List<DoctorDto>> GetAllDoctoresSustitutosAsync();
        Task<UsuarioPerfilDto> GetMyPerfilasync(string UserId);
        Task<bool> UpdateEmpleadoAsync(UpdateEmployeeDto dto);
        Task<bool> DeleteEmpleadoAsync(DeleteEmployeeDTO deleteEmployee);
        Task<bool> ActivarEmpleadoAsync(int EmployeeId);

    }
}
