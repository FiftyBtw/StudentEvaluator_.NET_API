using Client_Model;

namespace EF_UnitTests.Model;


public class StudentTests
{
    [Fact]
    public void ConstructorAssignsProperties()
    {
        // Arrange
        var id = 1L;
        var name = "Jane";
        var lastname = "Doe";
        var urlPhoto = "http://example.com/jane.jpg";
        var groupYear = 1;
        var groupNumber = 1;

        // Act
        var student = new Student(id, name, lastname, urlPhoto, groupYear, groupNumber);

        // Assert
        Assert.Equal(id, student.Id);
        Assert.Equal(name, student.Name);
        Assert.Equal(lastname, student.Lastname);
        Assert.Equal(urlPhoto, student.UrlPhoto);
        Assert.Equal(groupYear, student.GroupYear);
        Assert.Equal(groupNumber, student.GroupNumber);
    }

    [Fact]
    public void PropertiesCanBeSet()
    {
        // Arrange
        var student = new Student(1, "InitialName", "InitialLastName", "http://example.com", 2020, 100);

        // Act
        student.Name = "UpdatedName";
        student.Lastname = "UpdatedLastName";
        student.UrlPhoto = "http://example.com/updated.jpg";
        student.GroupYear = 1;
        student.GroupNumber = 1;

        // Assert
        Assert.Equal("UpdatedName", student.Name);
        Assert.Equal("UpdatedLastName", student.Lastname);
        Assert.Equal("http://example.com/updated.jpg", student.UrlPhoto);
        Assert.Equal(1, student.GroupYear);
        Assert.Equal(1, student.GroupNumber);
    }

    [Fact]
    public void ToStringReturnsExpectedFormat()
    {
        // Arrange
        var student = new Student(1, "Jane", "Doe", "http://example.com/jane.jpg", 1, 1);

        // Act
        var result = student.ToString();

        // Assert
        var expected = "Student : 1, Jane Doe, 1A G1.";
        Assert.Equal(expected, result);
    }
}

