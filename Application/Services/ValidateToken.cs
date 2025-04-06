using Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class ValidateToken
    {

        public static TokenResult validate(string? Token, string[] roleRequired)
        {
            
            if (string.IsNullOrEmpty(Token))
            {
                throw new Exception("Token esta vacio");
            }
            var authHeader = Token.Replace("Bearer ", "");
            var stream = authHeader;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var Email = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var UserId = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            var RoleName = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

            bool isValid = false;
            if (roleRequired.Contains(RoleName))
            {
                isValid = true;
            }
            return new TokenResult {
                UserId = UserId,
                RoleName = RoleName,
                Email = Email,
                IsValidUser = isValid
            };
        }
        public static string[] GetRoles()
        {

            string[] roles = {
                "Admin", "DoctorSustituto", "DoctorInterino", "DoctorTitular",
                "AuxEnfermeria","ATS", "ATSZona","Celadores"};
            return roles;

        }
    }
}
