namespace API_Dto;

public class LoginResponseDto
{
    public string Username { get; set; }
    public string[] Roles { get; set; }
    public long Id { get; set; }  
}