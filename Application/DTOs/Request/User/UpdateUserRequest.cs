using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request.User
{
    public class UpdateUserRequest
    {
        public required string UserId { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ModifyUserId { get; set; }

    }
}
