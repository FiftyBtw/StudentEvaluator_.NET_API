using Client_Model;

namespace EF_UnitTests.Model
{
    public class UserTests
    {
        [Fact]
        public void TestUserCreation()
        {
            // Arrange
            var id = "1";
            var username = "testUser";
            var password = "password";

            // Act
            var user = new User(id, username, password);

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(username, user.Username);
            Assert.Equal(password, user.Password);
        }

        [Fact]
        public void TestUserToString()
        {
            // Arrange
            var user = new User("2", "JaneDoe", "secret");
            var expectedString = "User : 2, JaneDoe\n"; // Note: The original implementation concatenates roles without separation.

            // Act
            var result = user.ToString();

            // Assert
            Assert.Equal(expectedString, result);
        }

        [Fact]
        public void TestUserDefaultConstructor()
        {
            // Arrange & Act
            var user = new User(); 

            // Assert
            Assert.Equal("0", user.Id);
            Assert.Equal("", user.Username); 
            Assert.Equal("", user.Password); 
        }
    }
}
