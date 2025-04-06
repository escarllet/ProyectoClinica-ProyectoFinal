
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Auth;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

using Application.DTOs.Request.User;


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
        //[HttpPost("assign-role")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        //{
        //    var success = await _authService.AssignRoleToUserAsync(request.UserId, request.RoleName);
        //    if (success) return Ok($"Rol '{request.RoleName}' asignado al usuario {request.UserId}.");
        //    return BadRequest("Error al asignar el rol.");
        //}
        

        //se buscan la lista de roles de los usuarios por el correo
        //ya funciona roles no
        //[HttpGet("RolesByMail")]
        //[AllowAnonymous]
        //public List <string>GetRolesByMail(string usermail)
        //{
        //    try
        //    {
        //        var roles =  _authService.GetUserRolesAsync(usermail).Result;
        //        return roles;
        //    }
        //    catch (Exception)
        //    {
        //        throw;

        //    }
           
        //}
        //Como Usuario del sistema, independientemente del rol, quiero iniciar sesión con mi correo
        //y contraseña para acceder al sistema.
        //ya funciona login devuelve token
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            var response = await _authService.Login(request);
            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }
        //Como administrador, quiero ver la lista de usuarios registrados para gestionar su información."
        //Tambien puede filtrar por correo
        // ya funciona
        [HttpGet("users")]

        public async Task<IActionResult> GetUsers(string? filtro = null)
        {
            try
            {
                string[] rols = ["Admin"];
                var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
                if (!a.IsValidUser)
                {
                    return Unauthorized("Usuario no tiene permisos para realiza esta accion");
                }
                var users = await _authService.GetAllUsersAsync(filtro);
                if (users.Count == 0)
                {
                    return Ok("No se encontro ningun usuario");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        //Como administrador, quiero poder editar la información de un usuario para mantener los datos actualizados. 
        //ya funciona
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            string[] rols = ["Admin"];
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            request.ModifyUserId = a.UserId;
            var success = await _authService.UpdateUserAsync(request);

            if (!success) return NotFound(new { message = "Usuario no encontrado" });

            return Ok(new { message = "Usuario actualizado correctamente" });
        }
        //Como administrador, quiero poder eliminar un usuario si ya no forma parte del centro de salud.
        // ya funciona
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserByUserId (string IdUser)
        {
            string[] rols = ["Admin"];
            var a = ValidateToken.validate(Request.Headers["Authorization"].ToString(), rols);
            if (!a.IsValidUser)
            {
                return Unauthorized("Usuario no tiene permisos para realiza esta accion");
            }
            var success = await _authService.DeleteUserAsync(a.UserId,IdUser);

            if (!success) return NotFound(new { message = "Usuario no encontrado" });

            return Ok(new { message = "Usuario Inactivado correctamente" });
        }
        //ya funciona
        [HttpPut("ActivarUser")]
        public async Task<IActionResult> ActivarUserByMail(string IdUser)
        {
            var success = await _authService.ActivarUserByMail(IdUser);

            if (!success) return NotFound(new { message = "Usuario no encontrado" });

            return Ok(new { message = "Usuario Activado correctamente" });
        }        
    }
}
