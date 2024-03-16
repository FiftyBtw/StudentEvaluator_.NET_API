using Client_Model;

namespace EF_UnitTests.Model;

public class LoginRequestTests
{
    [Fact]
    public void TestCreateLoginRequest()
    {
        // Arrange
        var username = "user";
        var password = "pass";

        // Act
        var request = new LoginRequest(username, password);


        // Assert
        Assert.Equal(username, request.Username);
        Assert.Equal(password, request.Password);
    }
}