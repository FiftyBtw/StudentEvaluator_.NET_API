namespace Client_Model
{
    public class User
    {
        private readonly string _id;
        public string Id { get { return _id; } }

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";
        
        public User() { }

        public User(string id, string username, string password)
        {
            _id = id;
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            string user = "User : " + Id + ", " + Username + "\n" + "\tRoles :";
            user += "\n";
            return user;
        }
    }
}
