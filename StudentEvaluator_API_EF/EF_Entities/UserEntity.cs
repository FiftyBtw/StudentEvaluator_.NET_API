namespace EF_Entities;


public class UserEntity
{
    public long Id { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string[] roles { get; set; }
}