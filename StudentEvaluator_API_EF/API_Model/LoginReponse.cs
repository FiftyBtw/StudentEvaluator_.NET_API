using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Model
{
    public class LoginReponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string[] Roles { get; set; }
        
        public LoginReponse(long id, string username, string[] roles) {
            Id = id;
            Username = username;
            Roles = roles;
        }
    }
}
