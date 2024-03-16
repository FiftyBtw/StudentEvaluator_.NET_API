namespace Client_Model
{
    public class LoginResponse(long id, string username, string[] roles)
    {
        public long Id { get; } = id;
        public string Username { get; } = username;
        public string[] Roles { get; } = roles;
    }
}
