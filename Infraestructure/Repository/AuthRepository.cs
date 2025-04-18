﻿using Microsoft.AspNetCore.Identity;
using Application.Contracts;
using Application.DTOs.Auth;
using Domain.Entities.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.Request.Employee;
using Microsoft.AspNetCore.Http;
using Application.DTOs.Response.User;
using Application.DTOs.Request.User;
using System.Data;


namespace Infraestructure.Repository
{
    public class AuthRepository : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;

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
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            return _userManager.GetRolesAsync(user).Result.ToList();
        }
        public async Task<AuthResponseDto> Login(AuthRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return new AuthResponseDto { Success = false, Message = "Credenciales inválidas" };
            if (!user.Activo)
                return new AuthResponseDto { Success = false, Message = "Usuario Inactivo" };

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = await GenerateJwtToken(user,userRoles.First());

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Rol = userRoles.First(),
                Message = "Login exitoso"
            };
        }
        private async Task<string> GenerateJwtToken(ApplicationUser user,string rol)
        {

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, rol)
            };

            //foreach (var role in userRoles[1])
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aG1zZGdmMDk4MjNhc2Rma2prbGg0NTY3OGZnZGg="));
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                expires: null,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }      
        public async Task<List<UserDto>> GetAllUsersAsync(string? filtro = null)
        {
            // Get the query with filter applied if needed
            var query = _userManager.Users.Where(u => u.Activo).AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(u => u.Email.Contains(filtro) || u.Id.Contains(filtro));
            }

            // Convert the query to a list of users (this loads all the users at once)
            var users = await query.OrderBy(c => c.Email).ToListAsync();

            // Initialize the list of user DTOs
            var userList = new List<UserDto>();

            // Process each user one at a time to avoid concurrent DbContext access
            foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                userList.Add(new UserDto
                {
                    Id = user.Id,
                    email = user.Email,
                    name = user.Name,
                    rol = role,
                    fechaCreacion = user.FechaCreacion,
                    username = user.UserName
                });
            }

            return userList;
        }

        //public async Task<List<UserDto>> GetAllUsersAsync(string? filtro = null)
        //{
        //    var query = _userManager.Users.Where(u => u.Activo).AsQueryable();

        //    if (!string.IsNullOrEmpty(filtro))
        //    {
        //        query = query.Where(u => u.Email.Contains(filtro) || u.Id.Contains(filtro));
        //    }
        //    List<UserDto> a = new List<UserDto>(); 
        //    foreach (var item in query)
        //    {
        //        var role = _userManager.GetRolesAsync(item).Result.FirstOrDefault();

        //        var user = query.Select(c => new UserDto
        //        {
        //            Id = c.Id,
        //            email = c.Email,
        //            name = c.Name,
        //            rol = role,
        //            fechaCreacion = c.FechaCreacion,
        //            username = c.UserName,             
        //        }).FirstOrDefault(v => v.email == item.Email);
        //        a.Add(user);
        //    }
        //    return a;
        //}

        public async Task<ApplicationUser> RegisterUserEmployeAsync(RegisterEmployeeDto dto)
        {
            // Verificar si el correo ya está registrado
            ApplicationUser? existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser == null || existingUser.Activo == false)
            {
                var user = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    Pass = dto.Password,
                    Activo = true,
                    Version = 1,
                    IdUsuarioCreacion = dto.UsuarioCreacion,
                    FechaCreacion = DateTime.Now,


                };
                if (!await _roleManager.RoleExistsAsync(dto.TipoEmpleado))
                {
                    throw new Exception("El Rol que intenta agregar no existe.");
                }
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                // Verificar si el rol existe, si no, Error
              


                // Asignar el rol al usuario
                await _userManager.AddToRoleAsync(user, dto.TipoEmpleado);
                var userCreated = await _userManager.FindByEmailAsync(user.Email);
                // Crear el empleado y asociarlo al usuario
                if (userCreated == null)
                {
                    throw new Exception("Usuario no ha sido creado");
                }


                return userCreated;
            }
            throw new Exception("El correo ya está registrado.");


            // Crear el usuario



        }

      
        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return false;

            user.Email = request.Email;
            user.Pass = request.Password;
            user.UserName = request.UserName;
            user.Name = request.Name;
            user.Version++;
            user.FechaModificacion = DateTime.Now;
            user.IdUsuarioModificacion = request.ModifyUserId;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> DeleteUserAsync(string IdUser, string DeleteUser)
        {
            var user = await _userManager.FindByIdAsync(DeleteUser);
            if (user == null || user.Activo == false) return false;

            user.Activo = false;
            user.FechaModificacion = DateTime.Now;
            user.IdUsuarioModificacion = IdUser;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task<bool> ActivarUserByMail(string IdUser)
        {
            var user = await _userManager.FindByIdAsync(IdUser);
            if (user == null || user.Activo == true) return false;

            user.Activo = true;


            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

    }
}
