namespace API_Dto;


/// <summary>
/// Data transfer object for login response.
/// </summary>
public class LoginResponseDto
{
    public string Username { get; set; }
    public string[] Roles { get; set; }
    public long Id { get; set; }  

}