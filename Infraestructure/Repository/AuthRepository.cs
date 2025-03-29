using Microsoft.AspNetCore.Identity;
using Application.Contracts;
using System.Threading.Tasks;
using Application.DTOs.Auth;
using Domain.Entities.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.Request.Employee;
using Domain.Entities.People;

namespace Infrastructure.Repository
{
    public class AuthRepository : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        //private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
   
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) return false;

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<List<string>> GetUserRolesAsync(string usermail)
        {
            var user = await _userManager.FindByEmailAsync(usermail);
            return user != null ? new List<string>(await _userManager.GetRolesAsync(user)) : new List<string>();
        }
        public async Task<AuthResponseDto> Login(AuthRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return new AuthResponseDto { Success = false, Message = "Credenciales inválidas" };
            if (!user.Activo)
                return new AuthResponseDto { Success = false, Message = "Usuario Inactivo" };
            
            var token = await GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Message = "Login exitoso"
            };
        }
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aG1zZGdmMDk4MjNhc2Rma2prbGg0NTY3OGZnZGg="));
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync(string? email = null)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => u.Email.Contains(email) && u.Activo);
            }

            return await query.ToListAsync();
        }
        public async Task<Employee> RegisterUserEmployeAsync(RegisterEmployeeDto dto)
        {
            // Verificar si el correo ya está registrado
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            throw new Exception("El correo ya está registrado.");


            // Crear el usuario
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Pass = dto.Password
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            // Verificar si el rol existe, si no, Error
            if (!await _roleManager.RoleExistsAsync(dto.TipoEmpleado))
            {
                throw new Exception("El Rol que intenta agregar no existe.");
            }

            // Asignar el rol al usuario
            await _userManager.AddToRoleAsync(user, dto.TipoEmpleado);

            // Crear el empleado y asociarlo al usuario
            var employee = new Employee
            {
                Name = dto.NombreCompleto,
                Address = dto.Direccion,
                Phone = dto.Telefono,
                PostalCode = dto.CodigoPostal,
                NIF = dto.NIF,
                IdProvincia = dto.IdProvincia,
                fechaEntrada = dto.FechaEntradaEmpleado,
                SocialSecurityNumber = dto.NumeroSeguridadSocial,
                CodigoEmpleado = dto.CodigoEmpleado,
                Activo = true,
                FechaCreacion = System.DateTime.Now,
                // Asociamos el usuario al empleado
            };
            return employee;
     

        }
        public async Task<bool> UpdateUserAsync(string userId, string email, string phoneNumber)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.Email = email;
            user.PhoneNumber = phoneNumber;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> DeleteUserAsync(string userMail)
        {
            var user = await _userManager.FindByEmailAsync(userMail);
            if (user == null) return false;

            user.Activo = false;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

    }
}
