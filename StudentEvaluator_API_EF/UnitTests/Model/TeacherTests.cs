using Client_Model;

namespace EF_UnitTests.Model;

public class TeacherTests
{
    [Fact]
    public void DefaultConstructor()
    {
        // Act
        var teacher = new Teacher();

        // Assert
        Assert.NotNull(teacher.Templates);
        Assert.Empty(teacher.Templates);
    }

    [Fact]
    public void ConstructorWithParams()
    {
        // Act
        var teacher = new Teacher("1", "teacher1", "pass");

        // Assert
        Assert.Equal("1", teacher.Id);
        Assert.Equal("teacher1", teacher.Username);
        Assert.Equal("pass", teacher.Password);
        Assert.Empty(teacher.Templates);
    }

    [Fact]
    public void ConstructorWithTemplates()
    {
        // Arrange
        var roles = new[] { "Teacher" };
        var templates = new List<Template> { new Template(1, "template1", []), new Template(2, "template2", []) };

        // Act
        var teacher = new Teacher("1", "teacher2", "pass", templates);

        // Assert
        Assert.Equal("1", teacher.Id);
        Assert.Equal("teacher2", teacher.Username);
        Assert.Equal("pass", teacher.Password);
        Assert.Equal(templates, teacher.Templates);
        Assert.NotEmpty(teacher.Templates);
        Assert.Equal(2, teacher.Templates.Count);
    }
}