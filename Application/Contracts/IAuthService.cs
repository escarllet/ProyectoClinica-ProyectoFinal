using Application.DTOs.Auth;
using Application.DTOs.Request.Employee;
using Domain.Entities.Authentication;
using Domain.Entities.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IAuthService
    {
        Task<bool> AssignRoleToUserAsync(string userId, string roleName);
        Task<bool> RoleExistsAsync(string roleName);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task<AuthResponseDto> Login(AuthRequestDto request);
        Task<Employee> RegisterUserEmployeAsync(RegisterEmployeeDto dto);
        Task<List<ApplicationUser>> GetAllUsersAsync(string? email = null);
        Task<bool> UpdateUserAsync(string userId, string email, string phoneNumber);
        Task<bool> DeleteUserAsync(string userId);
    }
}
