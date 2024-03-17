using API_Dto;
using Asp.Versioning;
using EventLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API_EF.Controllers.V1;

/// <summary>
///  Controller for users
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService<UserDto,LoginRequestDto,LoginResponseDto> _userService;
    
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService<UserDto, LoginRequestDto, LoginResponseDto> userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    ///  Get all users
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetUsers(int index = 0, int count = 10)
    {
        _logger.LogInformation(LogEvents.GetItems, "GetUsers");
        if (_userService == null) return StatusCode(500);

        var data = await _userService.GetUsers(index, count);
        if (data == null)       
        {
            _logger.LogInformation(LogEvents.GetItems, "NoContent");
            return NoContent();
        }
        else
        {
            return Ok(data);
        }

    }

    /// <summary>
    ///  Get a user by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(long id)
    {
        _logger.LogInformation(LogEvents.GetItem, "GetUserById");

        if (_userService == null)return StatusCode(500);

        var user = await _userService.GetUserById(id);
        if(user == null)
        {
            _logger.LogInformation(LogEvents.GetItem, "NotFound");
            return NotFound();
        }
        else return Ok(user);
    }

    /// <summary>
    ///  Create a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] UserDto user)
    {
        _logger.LogInformation(LogEvents.InsertItem, "PostUser");
        if (_userService == null)return StatusCode(500);
        var userDto = await _userService.PostUser(user);
        if (userDto == null)
        {
            return BadRequest();
        }
        else
        {
            return Ok(userDto);
        }
    }

    /// <summary>
    ///  Update a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(long id, [FromBody] UserDto user)
    {
        _logger.LogInformation(LogEvents.UpdateItem, "PutUser");
        if (_userService == null)return StatusCode(500);
        var userDto = await _userService.PutUser(id, user);
        if (userDto == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(userDto);
        }
    }

    /// <summary>
    ///  Delete a user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        _logger.LogInformation(LogEvents.DeleteItem, "DeleteUser");
        if (_userService == null)return StatusCode(500);
      
        var result = await _userService.DeleteUser(id);
        if (result)
        {
            return Ok(result);
        }
        else
        {
            return NotFound();
        }
    }
    
    /// <summary>
    ///  Login (authenticate a user and send his data if successful, this login is deprecated because it is not secure, use the login with token instead)
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        _logger.LogInformation(LogEvents.GetItem, "Login");
        if (_userService == null)return StatusCode(500);
       
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