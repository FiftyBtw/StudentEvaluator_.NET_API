using API_Dto;
using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(int index = 0, int count = 10)
    {
        if (_userService == null)
        {
            return StatusCode(500);
        }
        var data = await _userService.GetUsers(index, count);
        if (data == null)       
        {
            return NoContent();
        }
        else
        {
            return Ok(data);
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(long id)
    {
        if (_userService == null)
        {
            return StatusCode(500);
        }
        var user = await _userService.GetUserById(id);
        if(user == null)
        {
            return NotFound();
        }
        else return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] UserDto user)
    {
        if (_userService == null)
        {
            return StatusCode(500);
        }
        var userDto = await _userService.PostUser(user);
        if (userDto == null)
        {
            return StatusCode(500);
        }
        else
        {
            return Ok(userDto);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(long id, [FromBody] UserDto user)
    {
        if (_userService == null)
        {
            return StatusCode(500);
        }
        var userDto = await _userService.PutUser(id, user);
        if (userDto == null)
        {
            return StatusCode(500);
        }
        else
        {
            return Ok(userDto);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        if (_userService == null)
        {
            return StatusCode(500);
        }
        var result = await _userService.DeleteUser(id);
        if (result)
        {
            return Ok();
        }
        else
        {
            return StatusCode(500);
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        if (_userService == null)
        {
            return StatusCode(500);
        }
        var loginResponse = await _userService.Login(loginRequest);
        if (loginResponse == null)
        {
            return Unauthorized();
        }
        else
        {
            return Ok(loginResponse);
        }
    }
}