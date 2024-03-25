using API_Dto;
using API_EF.Token;
using Asp.Versioning;
using EF_Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AccountController : ControllerBase
{
    
    private readonly UserManager<TeacherEntity> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<TeacherEntity> _signInManager;
    
    public AccountController(UserManager<TeacherEntity> userManager, ITokenService tokenService, SignInManager<TeacherEntity> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto register)
    {
        try
        {
            var user = new TeacherEntity
            {
                UserName = register.Username
            };
            var createdUser = await _userManager.CreateAsync(user, register.Password);
            if (createdUser.Succeeded)
            {
                var roles = await _userManager.AddToRoleAsync(user, "Teacher");
                if (roles.Succeeded)
                {
                    return Ok("User created");
                }
                return StatusCode(500, roles.Errors);
            }
            return StatusCode(500, createdUser.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userManager.FindByNameAsync(login.Username);

        if (user == null)
        {
            return Unauthorized("invalid username");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        
        if (!result.Succeeded)
        {
            return Unauthorized("Username or password is incorrect.");
        }
        var token = _tokenService.CreateToken(user);
        return Ok(new { token });
    }
}