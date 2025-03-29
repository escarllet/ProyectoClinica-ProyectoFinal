using Application.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Auth;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs.Request.Employee;
using Application.DTOs.Request.User;

namespace API.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var success = await _authService.AssignRoleToUserAsync(request.UserId, request.RoleName);
            if (success) return Ok($"Rol '{request.RoleName}' asignado al usuario {request.UserId}.");
            return BadRequest("Error al asignar el rol.");
        }
        //se busca la lista de roles
        [HttpGet("GetRolNames")]
        [AllowAnonymous]
        public IActionResult GetRoles()
        {
            var roles = _authService.GetRoles();
            return Ok(roles);
        }

        //se buscan la lista de roles de los usuarios por el correo
        [HttpGet("RolesByMail")]
        [AllowAnonymous]
        public IActionResult GetRolesByMail(string usermail)
        {
            var roles = _authService.GetUserRolesAsync(usermail);
            return Ok(roles);
        }
        //Como Usuario del sistema, independientemente del rol, quiero iniciar sesión con mi correo
        //y contraseña para acceder al sistema.
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            var response = await _authService.Login(request);
            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }
        //Como administrador, quiero ver la lista de usuarios registrados para gestionar su información."
        //Tambien puede filtrar por correo
        [HttpGet("users")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> GetUsers(string? email = null)
        {
            var users = await _authService.GetAllUsersAsync(email);
            return Ok(users);
        }
        //Como administrador, quiero poder editar la información de un usuario para mantener los datos actualizados. 
        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserRequest request)
        {
            var success = await _authService.UpdateUserAsync(userId, request.Email, request.PhoneNumber);

            if (!success) return NotFound(new { message = "Usuario no encontrado" });

            return Ok(new { message = "Usuario actualizado correctamente" });
        }
        //Como administrador, quiero poder eliminar un usuario si ya no forma parte del centro de salud.
        [HttpDelete("{userMail}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserByMail (string userMail)
        {
            var success = await _authService.DeleteUserAsync(userMail);

            if (!success) return NotFound(new { message = "Usuario no encontrado" });

            return Ok(new { message = "Usuario Inactivado correctamente" });
        }
    }
}
