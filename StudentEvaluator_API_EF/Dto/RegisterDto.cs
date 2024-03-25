using System.ComponentModel.DataAnnotations;

namespace API_Dto;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}