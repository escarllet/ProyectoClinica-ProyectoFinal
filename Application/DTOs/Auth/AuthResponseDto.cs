using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public required bool Success { get; set; }
        public string? Token { get; set; }
        public string? Rol { get; set; }
        public required string Message { get; set; }
    }

}
