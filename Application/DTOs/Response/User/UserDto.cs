using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Response.User
{
    public class UserDto
    {
        public required string Id { get; set; }

        public required string name { get; set; }

        public required DateTime fechaCreacion { get; set; }
        public required string username { get; set; }

        public required string email { get; set; }
        public required string phoneNumber { get; set; }
    }
}
