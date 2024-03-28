using API_Dto;
using Moq;
using Shared;
using API_EF.Controllers.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_EF.Controllers;

namespace EF_UnitTests.WebApi;

public class UserTests
{
    /*
    private readonly Mock<IUserService<UserDto,LoginDto,LoginResponseDto>> _mockRepo=new();
    private readonly Mock<ILogger<UsersController>> _mockLogger=new();
    private readonly UsersController _usersController;

    
    public UserTests()
    {     
        _usersController= new UsersController(_mockRepo.Object,_mockLogger.Object);
    }

    [Fact]
    public async void TestAddUser_OkResult()
    {
        // Arrange
        var teacher = new TeacherDto(){
            Id="1",
            Username="ProfDupuit",
            Password="test",};

        _mockRepo.Setup(x => x.PostUser(It.IsAny<UserDto>()))
                          .ReturnsAsync(teacher);

        // Act
        var result = await _usersController.PostUser(userDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUserDto = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(userDto.Id, returnedUserDto.Id);
        Assert.Equal(userDto.Username, returnedUserDto.Username);
        Assert.Equal(userDto.Password, returnedUserDto.Password);
        Assert.Equal(userDto.roles, returnedUserDto.roles);
    }

    [Fact]
    public async void TestAddUser_BadRequest()
    {
        // Arrange
        _mockRepo.Setup(x => x.PostUser(It.IsAny<UserDto>()))
                          .ReturnsAsync((UserDto)null);
        var userDto = new UserDto(); 

        // Act
        var result = await _usersController.PostUser(userDto) as BadRequestResult;

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }


    [Fact]
    public async void TestDeleteUser_OkResult()
    {
        // Arrange
        _mockRepo.Setup(x => x.DeleteUser(It.IsAny<long>()))
                          .ReturnsAsync(true);
        long teacherId = 1; 

        // Act
        var result = await _usersController.DeleteUser(teacherId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<bool>(okResult.Value);
        Assert.True(returnValue);
    }


    [Fact]
    public async void TestDeleteUser_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.DeleteUser(It.IsAny<long>()))
            .ReturnsAsync(false); 

        // Act
        var result = await _usersController.DeleteUser(123); 

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void TestUpdateUser_OkResult()
    {
        // Arrange

        _mockRepo.Setup(service => service.PutUser(It.IsAny<long>(), It.IsAny<UserDto>()))
            .ReturnsAsync((long id, UserDto user) => user);

        // Act
        var result = await _usersController.PutUser(123, new UserDto());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudent = Assert.IsType<UserDto>(okResult.Value);
    }
  

    [Fact]
    public async void TestGetUsers_OkResult()
    {
        // Arrange
        var users = new List<UserDto>
        {
            new UserDto{
                Id=1,
                Username="ProfDupuit",
                Password="test",
                roles=[]
            },
             new UserDto{
                Id=2,
                Username="ProfMarc",
                Password="test2",
                roles=[]
            },

        };
        var pageResponse = new PageReponse<UserDto>(users.Count, users);

        _mockRepo.Setup(service => service.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await _usersController.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPageResponse = Assert.IsType<PageReponse<UserDto>>(okResult.Value);
        Assert.Equal(users.Count, returnedPageResponse.Data.Count());
    }

    [Fact]
    public async void TestGetUsers_NotContent()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetUsers(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((PageReponse<UserDto>)null);

        // Act
        var result = await _usersController.GetUsers();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void TestUpdateUser_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.PutUser(It.IsAny<long>(), It.IsAny<UserDto>()))
            .ReturnsAsync((long id, UserDto user) => null); 

        // Act
        var result = await _usersController.PutUser(123, new UserDto());

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async void TestGetUserById_OkResult()
    {
        // Arrange
        var userId = 123; 
        var existingUser = new UserDto { Id = userId, Username = "ProfDupuit", Password = "test", roles = [] };

        _mockRepo.Setup(service => service.GetUserById(It.IsAny<long>()))
            .ReturnsAsync((long id) => id == userId ? existingUser : null); 

        // Act
        var result = await _usersController.GetUserById(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUser = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(existingUser.Id, returnedUser.Id);
        Assert.Equal(existingUser.Username, returnedUser.Username);
        Assert.Equal(existingUser.Password, returnedUser.Password);
        Assert.Equal(existingUser.roles, returnedUser.roles);
    }

 
    [Fact]
    public async void TestGetUserById_NotFoundResult()
    {
        // Arrange
        _mockRepo.Setup(service => service.GetUserById(It.IsAny<long>()))
            .ReturnsAsync((long id) => null);

        // Act
        var result = await _usersController.GetUserById(123);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void TestLogin_UnAuthorized()
    {
        // Arrange
        _mockRepo.Setup(service => service.Login(It.IsAny<LoginDto>()))
            .ReturnsAsync((LoginDto loginRequestDto) => null);

        // Act
        var result = await _usersController.Login(new LoginDto
        {
            Username = "adjaziodjazodi",
            Password = "adiazoidjazdioazd"
        });

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async void TestLogin_OkResult()
    {
        // Arrange
        var loginRequestDto = new LoginDto
        {
            Username = "ProfDupuit",
            Password ="test"

        };
        _mockRepo.Setup(service => service.Login(It.IsAny<LoginDto>()))
            .ReturnsAsync(new LoginResponseDto
            {
                Id = 1,
                Username="ProfDupuit",
                Roles = []
            });

        // Act
        var result = await _usersController.Login(loginRequestDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedLoginReponse = Assert.IsType<LoginResponseDto>(okResult.Value);
        Assert.Equal(loginRequestDto.Username, returnedLoginReponse.Username);
    }

    */
}

