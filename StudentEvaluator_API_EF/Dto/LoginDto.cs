using System.ComponentModel.DataAnnotations;

namespace API_Dto;

/// <summary>
/// Data transfer object for login request.
/// </summary>
public class LoginDto
{
    [Required]
    public string? Username { get; set; }
    
    [Required]
    public string? Password { get; set; }

}