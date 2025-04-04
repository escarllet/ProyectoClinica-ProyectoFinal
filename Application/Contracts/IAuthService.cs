using Application.DTOs.Auth;
using Application.DTOs.Request.Employee;
using Application.DTOs.Request.User;
using Application.DTOs.Response.User;
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
        Task<List<string>> GetUserRolesAsync(string usermail);

        Task<AuthResponseDto> Login(AuthRequestDto request);
        Task<ApplicationUser> RegisterUserEmployeAsync(RegisterEmployeeDto dto);
        Task<bool> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeleteUserAsync(string userId);
        string? ObtenerUserIdActual();
        Task<bool> ActivarUserByMail(string userMail);
        Task<List<UserDto>> GetAllUsersAsync(string? filtro = null);
    }
}
