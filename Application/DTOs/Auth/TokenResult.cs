using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class TokenResult
    {
        public required string UserId { get; set; }

        public required string RoleName { get; set; }
        public required string Email { get; set; }
        public required bool IsValidUser { get; set; }
    }
}
