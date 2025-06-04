using Moq;
using Microsoft.Extensions.Configuration;
using EF_Entities;
using API_EF.Token;


namespace Token.Tests
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            // Mock IConfiguration
            _configMock = new Mock<IConfiguration>();
            _configMock.SetupGet(x => x["JWT:SigningKey"]).Returns("YFUDGUDGYG8367745I7GUBD30984B6478GBFNIFOjenfhefhIY736GBçdb/fghjkkjhgfghjHHH==");
            _configMock.SetupGet(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _configMock.SetupGet(x => x["JWT:Audience"]).Returns("http://localhost:5000");

            // Initialize TokenService with mock IConfiguration
            _tokenService = new TokenService(_configMock.Object);
        }

        [Fact]
        public void CreateToken_Returns_Valid_Token()
        {
            // Arrange
            var user = new TeacherEntity
            {
                Id = "123",
                UserName = "testuser"
            };

            // Act
            var token = _tokenService.CreateToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
    }
}