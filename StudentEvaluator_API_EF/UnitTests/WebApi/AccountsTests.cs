using API_Dto;
using API_EF.Controllers.V1;
using API_EF.Token;
using Client_Model;
using EF_Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EF_UnitTests.WebApi
{
    public class TestsCriteriaModelConverter
    {
        private readonly Mock<UserManager<TeacherEntity>> _mockUserManager= new Mock<UserManager<TeacherEntity>>(Mock.Of<IUserStore<TeacherEntity>>(), null, null, null, null, null, null, null, null);
        private readonly Mock<ITokenService> _mockTokenService = new();
        private readonly Mock<IHttpContextAccessor> mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        private readonly Mock<IUserClaimsPrincipalFactory<TeacherEntity>> mockUserClaimsPrincipalFactory = new();
        private readonly IOptions<IdentityOptions> _identityOptions = Options.Create(new IdentityOptions());
        private readonly Mock<SignInManager<TeacherEntity>> _mockSignInManager;
        private readonly AccountController _accountsController;

        public TestsCriteriaModelConverter()
        {
            _mockSignInManager = new Mock<SignInManager<TeacherEntity>>(_mockUserManager.Object,mockHttpContextAccessor.Object, mockUserClaimsPrincipalFactory.Object, _identityOptions, null, null, null);
                                                            
            _accountsController = new AccountController(_mockUserManager.Object,_mockTokenService.Object,_mockSignInManager.Object);
        }

        [Fact]
        public async void TestRegister_OkResult()
        {
            var registerDto = new RegisterDto
            {
                Username = "testuser",
                Password = "testpassword"
            };
            _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<TeacherEntity>(), registerDto.Password))
                            .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<TeacherEntity>(), "Teacher"))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _accountsController.Register(registerDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User created", result.Value);
        }

        [Fact]
        public async Task TestRegister_ReturnsInternalServerError()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Username = "testuser",
                Password = "testpassword"
            };
            _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<TeacherEntity>(), registerDto.Password))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "User creation failed" }));

            // Act
            var result = await _accountsController.Register(registerDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
   
        }


        [Fact]
        public async Task TestLogin_InvalidUsername_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Username = "invalidusername",
                Password = "testpassword"
            };
            _mockUserManager.Setup(m => m.FindByNameAsync(loginDto.Username))
                            .ReturnsAsync((TeacherEntity)null);

            // Act
            var result = await _accountsController.Login(loginDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
            Assert.Equal("invalid username", result.Value);
        }
    


        [Fact]
        public async Task TestLogin_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Username = "validusername",
                Password = "validpassword"
            };
            var user = new TeacherEntity { UserName = loginDto.Username };
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            _mockUserManager.Setup(m => m.FindByNameAsync(loginDto.Username))
                            .ReturnsAsync(user);
            _mockSignInManager.Setup(m => m.CheckPasswordSignInAsync(user, loginDto.Password, false))
                              .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            _mockTokenService.Setup(m => m.CreateToken(user))
                             .Returns("testtoken");

            // Act
            var result = await _accountsController.Login(loginDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Contains("testtoken", result.Value.ToString());
        }

        [Fact]
        public async Task TestGetUserInfo_AuthorizedUser_ReturnsUserInfo()
        {
            // Arrange
            var user = new TeacherEntity { UserName = "testuser", Id = "123" };
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(a => a.HttpContext.User).Returns(principal);

            _mockUserManager.Setup(m => m.FindByNameAsync(user.UserName))
                            .ReturnsAsync(user);

            _accountsController.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContextAccessor.Object.HttpContext
            };

            // Act
            var result = await _accountsController.GetUserInfo() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var userInfo = result.Value as UserInfoDto;
            Assert.NotNull(userInfo);
            Assert.Equal(user.Id, userInfo.Id);
            Assert.Equal(user.UserName, userInfo.Username);
        }

        [Fact]
        public async Task TestGetUserInfo_UnauthorizedUser_ReturnsUnauthorized()
        {
            // Arrange
            var claims = new List<Claim>(); // Empty claims, indicating no user is authenticated
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(a => a.HttpContext.User).Returns(principal);

            _accountsController.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContextAccessor.Object.HttpContext
            };

            // Act
            var result = await _accountsController.GetUserInfo() as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }


    }

}
