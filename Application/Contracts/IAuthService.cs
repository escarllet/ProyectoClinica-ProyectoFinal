using Application.DTOs.Auth;
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
    }
}
