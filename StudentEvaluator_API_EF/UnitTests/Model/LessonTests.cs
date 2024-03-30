using Client_Model;

namespace EF_UnitTests.Model;

public class LessonTests
{
    [Fact]
    public void TestConstructor()
    {
        // Arrange
        var id = 1L;
        var start = new DateTime(2024, 3, 16, 9, 0, 0);
        var end = new DateTime(2024, 3, 16, 11, 0, 0);
        var courseName = "Mathematics";
        var classroom = "A19";
        var teacher = new Teacher("1", "ProfSmith", "password");
        var group = new Group(1, 1, new List<Student>());

        // Act
        var lesson = new Lesson(id, start, end, courseName, classroom, teacher, group);

        // Assert
        Assert.Equal(id, lesson.Id);
        Assert.Equal(start, lesson.Start);
        Assert.Equal(end, lesson.End);
        Assert.Equal(courseName, lesson.CourseName);
        Assert.Equal(classroom, lesson.Classroom);
        Assert.Equal(teacher, lesson.Teacher);
        Assert.Equal(group, lesson.Group);
    }
    
    [Fact]
    public void TestDefaultConstructor()
    {
        // Act
        var lesson = new Lesson();

        // Assert
        Assert.Equal(0, lesson.Id);
        Assert.Equal(new DateTime(), lesson.Start);
        Assert.Equal(new DateTime(), lesson.End);
        Assert.Equal("", lesson.CourseName);
        Assert.Equal("", lesson.Classroom);
    }

    [Fact]
    public void TestToString()
    {
        // Arrange
        var lesson = new Lesson(
            2,
            new DateTime(2024, 3, 16, 10, 0, 0),
            new DateTime(2024, 3, 16,12, 0, 0),
            "Mathematics",
            "A19",
            new Teacher("2", "ProfJohnson", "secure"),
            new Group(2, 1, new List<Student>())
        );
        var expectedFormat = $"Lesson : 2, Mathematics,A19, {new DateTime(2024, 3, 16, 10, 0, 0)}-{new DateTime(2024, 3, 16, 12, 0, 0)}\n" +
                             $"\tTeacher : ProfJohnson\n" +
                             $"\tGroup  :2A G1\n";

        // Act
        var toStringOutput = lesson.ToString();

        // Assert
        Assert.Equal(expectedFormat, toStringOutput);
    }
}
