using Application.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Auth;
using Application.Services;

namespace API.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var success = await _authService.AssignRoleToUserAsync(request.UserId, request.RoleName);
            if (success) return Ok($"Rol '{request.RoleName}' asignado al usuario {request.UserId}.");
            return BadRequest("Error al asignar el rol.");
        }

        [HttpGet("user-roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var roles = await _authService.GetUserRolesAsync(userId);
            return Ok(roles);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            var response = await _authService.Login(request);
            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }

    }
}
