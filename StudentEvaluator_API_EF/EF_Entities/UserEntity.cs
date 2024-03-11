namespace EF_Entities;

/// <summary>
/// Represents a user entity in the database.
/// </summary>
public class UserEntity
{
    public long Id { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string[] Roles { get; set; }
}