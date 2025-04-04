using Application.Contracts;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.DTOs.Request.Employee;
using Application.DTOs.Response.User;
using Application.DTOs.Request.User;


namespace Application.Services
{
    public class AuthService 
    {
        private readonly IAuthService _authService;

        public AuthService(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<bool> AssignRoleToUserAsync(string userId, string roleName)
        {
            return _authService.AssignRoleToUserAsync(userId, roleName);
        }

        public Task<bool> RoleExistsAsync(string roleName)
        {
            return _authService.RoleExistsAsync(roleName);
        }

        public Task<List<string>> GetUserRolesAsync(string usermail)
        {
            return _authService.GetUserRolesAsync(usermail);
        } 
       

        public Task<AuthResponseDto> Login(AuthRequestDto request)
        {
            return _authService.Login(request);
        }

        public async Task<List<UserDto>> GetAllUsersAsync(string? email = null)
        {
            return await _authService.GetAllUsersAsync(email);
        }
        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            return await _authService.UpdateUserAsync(request);
        }
        public async Task<bool> DeleteUserAsync(string IdUser)
        {
            return await _authService.DeleteUserAsync(IdUser);
        }
        public async Task<bool> ActivarUserByMail(string IdUser)
        {
            return await _authService.ActivarUserByMail(IdUser);
        }

    }
}
