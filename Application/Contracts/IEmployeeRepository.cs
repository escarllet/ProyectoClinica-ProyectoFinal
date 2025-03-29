using Application.DTOs.Request.Employee;
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
        Task<List<Employee>> GetAllEmployeeAsync();
        Task<string> RegisterEmployeeAsync(RegisterEmployeeDto dto);
        Task<List<Doctor>> GetAllDoctoresAsync();

    }
}
