namespace Client_Model
{
    public class User
    {
        private readonly long _id;
        public long Id { get { return _id; } }

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public string[] Roles { get; set; } = [];

        public User() { }

        public User(long id, string username, string password, string[] roles)
        {
            _id = id;
            Username = username;
            Password = password;
            this.Roles = roles;
        }

        public override string ToString()
        {
            string user = "User : " + Id + ", " + Username + "\n" + "\tRoles :";
            foreach (var role in Roles)
            {
                user += role;
               
            }
            user += "\n";
            return user;
        }
    }
}
