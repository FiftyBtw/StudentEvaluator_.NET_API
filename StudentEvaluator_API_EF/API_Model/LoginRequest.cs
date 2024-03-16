namespace Client_Model
{
    public class LoginRequest(string username, string password)
    {
        public string Username { get; } = username;
        public string Password { get; } = password;
    }
}
