using Client_Model;

namespace EF_UnitTests.Model;

public class LoginResponseTests
{
    [Fact]
    public void Constructor_Assigns_Properties_Correctly()
    {
        // Arrange
        var id = 1L;
        var username = "user";
        var roles = new[] { "admin", "user" };

        // Act
        var response = new LoginResponse(id, username, roles);

        // Assert
        Assert.Equal(id, response.Id);
        Assert.Equal(username, response.Username);
        Assert.Equal(roles, response.Roles);
    }
}