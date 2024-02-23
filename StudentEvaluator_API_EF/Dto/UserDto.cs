using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class UserDto
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string[] roles { get; set; }
    }
}
