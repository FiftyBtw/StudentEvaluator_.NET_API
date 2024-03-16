using Client_Model;

namespace EF_UnitTests.Model;

public class LessonCreationTests
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
       

        // Act
        var lesson = new LessonCreation(start, end, courseName, classroom, 1, 1, 1);

        // Assert
        Assert.Equal(start, lesson.Start);
        Assert.Equal(end, lesson.End);
        Assert.Equal(courseName, lesson.CourseName);
        Assert.Equal(classroom, lesson.Classroom);
        Assert.Equal(1, lesson.TeacherId);
        Assert.Equal(1, lesson.GroupYear);
        Assert.Equal(1, lesson.GroupNumber);
    }
    
    [Fact]
    public void TestDefaultConstructor()
    {
        // Act
        var lesson = new LessonCreation();
        
        // Assert
        Assert.Equal(new DateTime(), lesson.Start);
        Assert.Equal(new DateTime(), lesson.End);
        Assert.Equal("", lesson.CourseName);
        Assert.Equal("", lesson.Classroom);
        Assert.Equal(0, lesson.TeacherId);
        Assert.Equal(0, lesson.GroupYear);
        Assert.Equal(0, lesson.GroupNumber);
    }
}