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

        public Task<List<string>> GetUserRolesAsync(string userId)
        {
            return _authService.GetUserRolesAsync(userId);
        }

        public Task<AuthResponseDto> Login(AuthRequestDto request)
        {
            return _authService.Login(request);
        }

    }
}
