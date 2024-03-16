using Xunit;
using Client_Model;
using System.Collections.Generic;
using System.Linq;

namespace EF_UnitTests.Model;

public class GroupTests
{
    [Fact]
    public void TestGroupCreation()
    {
        // Arrange & Act
        var group = new Group(1, 1);

        // Assert
        Assert.Equal(1, group.GroupYear);
        Assert.Equal(1, group.GroupNumber);
        Assert.Empty(group.Students);
    }

    [Fact]
    public void TestGroupCreationWithStudents()
    {
        // Arrange
       var students = new List<Student>
        {
            new Student(1, "John", "Doe", "url", 1, 1)
        };

        // Act
        var group = new Group(1, 1, students);

        // Assert
        Assert.Equal(1, group.GroupYear);
        Assert.Equal(1, group.GroupNumber);
        Assert.Equal(students.Count(), group.Students.Count);
        Assert.Contains(students.First(), group.Students);
    }

    [Fact]
    public void TestGroupToString()
    {
        // Arrange
        var group = new Group(1, 1);

        // Act
        var result = group.ToString();

        // Assert
        var expected = "Group : 1A G1 :\n";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestGroupToStringWithStudents()
    {
        // Arrange
        var students = new List<Student>
        {
            new Student(1, "John", "Doe", "url", 1, 1)
        };
        var group = new Group(1, 1, students);

        // Act
        var result = group.ToString();

        // Assert
        var expected = "Group : 1A G1 :\n" + string.Join("\n", students.Select(s => s.ToString())) + "\n";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestGroupDefaultConstructor()
    {
        // Arrange & Act
        var group = new Group();

        // Assert
        Assert.Equal(0, group.GroupYear);
        Assert.Equal(0, group.GroupNumber);
        Assert.Empty(group.Students);
    }
}
