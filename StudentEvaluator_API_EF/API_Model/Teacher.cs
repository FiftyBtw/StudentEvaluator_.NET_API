using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API_Model
{
    public class Teacher : User
    {
        public Teacher() { }

        public Teacher(long id, string username, string password, string[] roles):base(id, username, password, roles)
        {

        }
    }
}
